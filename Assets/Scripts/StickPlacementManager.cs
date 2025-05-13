using System.Collections.Generic;
using UnityEngine;

public class StickPlacementManager : MonoBehaviour
{
    public static StickPlacementManager Instance;
    public GameObject stickHorizontalPrefab;
    public GameObject stickVerticalPrefab;
    public GameObject squareHighlightPrefab;
    
    private HashSet<Edge> placedSticks = new HashSet<Edge>();
    private HashSet<Vector2Int> completedSquares = new HashSet<Vector2Int>();
    private int comboLevel = 0;
    private Dictionary<Edge, GameObject> stickVisuals = new Dictionary<Edge, GameObject>();
    private Dictionary<Vector2Int, GameObject> squareVisuals = new Dictionary<Vector2Int, GameObject>();

    private void Awake(){
        Instance = this;
    }

    public void CheckClosedSquare()
    {
        int squareCount = 0;
        int scoreGain = 0;
        Vector3 lastClosedSquareCenter = Vector3.zero;

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);

                if (completedSquares.Contains(cell)) continue;

                Vector2Int tl = new Vector2Int(x, y);
                Vector2Int tr = new Vector2Int(x + 1, y);
                Vector2Int bl = new Vector2Int(x, y + 1);
                Vector2Int br = new Vector2Int(x + 1, y + 1);

                bool top = placedSticks.Contains(new Edge(tl, tr));
                bool right = placedSticks.Contains(new Edge(tr, br));
                bool bottom = placedSticks.Contains(new Edge(bl, br));
                bool left = placedSticks.Contains(new Edge(tl, bl));

                if (top && right && bottom && left)
                {
                    completedSquares.Add(cell);
                    squareCount++;

                    Vector3 center = new Vector3(x + 0.5f, y + 0.5f, 0);
                    lastClosedSquareCenter = center;

                    GameObject visualSquare = Instantiate(squareHighlightPrefab, center, Quaternion.identity, this.transform);
                    squareVisuals[cell] = visualSquare;

                    ThemeData theme = LevelColorManager.Instance.Theme.GetThemeForLevel(LevelManager.Instance.CurrentLevel);
                    visualSquare.GetComponent<SpriteRenderer>().color = theme.highlightColor;
                    visualSquare.GetComponent<SquareHighlightEffect>()?.StartFlashEffect();

                    // Artan puan: 10 + 20 + 30 + ...
                    int perSquareScore = squareCount * 10;
                    if(comboLevel > 0){scoreGain += (comboLevel + 1) * perSquareScore;}else{scoreGain += perSquareScore;}    
                    
                    FloatingTextManager.Instance.ShowText($"+{squareCount * 10}", visualSquare.transform.position, Color.yellow);

                    SFXManager.Instance.PlaySquare();

                    // Stats
                    GameStatsTracker.Instance.AddSquares(squareCount);
                }
            }
        }

        if (squareCount > 0)
        {
            comboLevel++;
            ScoreManager.Instance.AddPoints(scoreGain);

            if (comboLevel > 1)
            {
                Vector3 topLeft = lastClosedSquareCenter + new Vector3(-0.5f, 0.5f, 0);
                FloatingImageManager.Instance.ShowComboImage(topLeft, comboLevel);
                // SFXManager.Instance.PlayCombo();
            }
        }
        else
        {
            comboLevel = 0;
        }
        
        CheckForFullLineClears();
    }



    public bool HasStick(Edge edge){
        return placedSticks.Contains(edge);
    }

    public void PlaceStickDirectly(Edge edge)
    {
        if (!placedSticks.Contains(edge))
        {
            placedSticks.Add(edge);

            Vector3 pointA = new Vector3(edge.A.x, edge.A.y, 0);
            Vector3 pointB = new Vector3(edge.B.x, edge.B.y, 0);
            Vector3 mid = (pointA + pointB) / 2f;
            float length = Vector3.Distance(pointA, pointB);

            bool isHorizontal = edge.A.y == edge.B.y;

            GameObject prefab = isHorizontal ? stickHorizontalPrefab : stickVerticalPrefab;
            GameObject stick = Instantiate(prefab, mid, Quaternion.identity);

            ThemeData theme = LevelColorManager.Instance.Theme.GetThemeForLevel(LevelManager.Instance.CurrentLevel);
            stick.GetComponent<SpriteRenderer>().color = theme.stickColor;

            stick.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            stickVisuals[edge] = stick;

            GridPoint a = GridManager.Instance.GetGridPointAt(edge.A);
            GridPoint b = GridManager.Instance.GetGridPointAt(edge.B);

            if (a != null) a.UpdateVisual(true);
            if (b != null) b.UpdateVisual(true);

            SFXManager.Instance.PlayPlace();
        }
    }

    public void CheckForFullLineClears(){
        List<int> fullRows = new List<int>();
        List<int> fullCols = new List<int>();

        for(int y = 0; y < 5; y++){
            bool full = true;
            for(int x = 0; x < 5; x++){
                if(!completedSquares.Contains(new Vector2Int(x,y))){
                    full = false;
                    break;
                }
            }
            if(full) fullRows.Add(y);
        }

        for(int x = 0; x < 5; x++){
            bool full = true;
            for(int y = 0; y < 5; y++){
                if(!completedSquares.Contains(new Vector2Int(x,y))){
                    full = false;
                    break;
                }
            }
            if(full) fullCols.Add(x);
        }

        foreach(int y in fullRows){
            for(int x = 0; x < 5; x++){
                ClearSquareAt(x, y);
            }
        }

        foreach(int x in fullCols){
            for(int y = 0; y < 5; y++){
                ClearSquareAt(x,y);
            }
        }

        if (fullRows.Count > 0 || fullCols.Count > 0)
        {
            Vector3 clearPos = new Vector3(2.5f, 2.5f, 0); // Grid'in ortası
            FloatingImageManager.Instance.ShowLineClearImage(clearPos);
            ShakeScreenEffect.Instance.ShakeScreen();
            FlashEffect.Instance.TriggerFlash();   
            SFXManager.Instance.PlayCompleteRawColumn();
            SFXManager.Instance.PlayExcellent();

            // Stats
            GameStatsTracker.Instance.AddLineClears(fullRows.Count + fullCols.Count);
        }
    }

    public void ClearSquareAt(int x, int y){
        Vector2Int cell = new Vector2Int(x,y);
        if(!completedSquares.Contains(cell)) return;

        completedSquares.Remove(cell);

        Vector2Int tl = new Vector2Int(x,y);
        Vector2Int tr = new Vector2Int(x+1,y);
        Vector2Int bl = new Vector2Int(x,y+1);
        Vector2Int br = new Vector2Int(x+1,y+1);

        List<Edge> edgesToRemove = new List<Edge>{
            new Edge(tl, tr),
            new Edge(tl,bl),
            new Edge(tr, br),
            new Edge(bl,br)
        };

        foreach(Edge e in edgesToRemove){
            if(placedSticks.Contains(e)){
                if (IsEdgeSharedWithOtherCompletedSquares(e, cell)) continue;

                placedSticks.Remove(e);

                if(stickVisuals.ContainsKey(e)){
                    Destroy(stickVisuals[e]);
                    stickVisuals.Remove(e);
                }

                UpdateGridPointsForEdge(e);
            }
        }

        if (squareVisuals.ContainsKey(cell))
        {
            Destroy(squareVisuals[cell]);
            squareVisuals.Remove(cell);
        }
    }

    private bool IsEdgeSharedWithOtherCompletedSquares(Edge edgeToCheck, Vector2Int exceptCell)
    {
        foreach (var cell in completedSquares)
        {
            if (cell == exceptCell) continue;

            Vector2Int tl = cell;
            Vector2Int tr = new Vector2Int(cell.x + 1, cell.y);
            Vector2Int bl = new Vector2Int(cell.x, cell.y + 1);
            Vector2Int br = new Vector2Int(cell.x + 1, cell.y + 1);

            List<Edge> edges = new List<Edge>
            {
                new Edge(tl, tr),
                new Edge(tr, br),
                new Edge(bl, br),
                new Edge(tl, bl)
            };

            if (edges.Contains(edgeToCheck))
                return true;
        }

        return false;
    }
    private void UpdateGridPointsForEdge(Edge e)
    {
        foreach (Vector2Int point in new[] { e.A, e.B })
        {
            bool stillConnected = HasAnyStickConnectedTo(point);
            GridPoint gp = GridManager.Instance.GetGridPointAt(point);
            if (gp != null)
                gp.UpdateVisual(stillConnected);
        }
    }

    private bool HasAnyStickConnectedTo(Vector2Int point)
    {
        foreach (Edge edge in placedSticks)
        {
            if (edge.A == point || edge.B == point)
                return true;
        }
        return false;
    }

    public void ResetBoard()
    {
        // Tüm kare görsellerini sil
        foreach (var square in squareVisuals.Values)
            Destroy(square);
        squareVisuals.Clear();
        completedSquares.Clear();

        // Tüm stickleri sil
        foreach (var stick in stickVisuals.Values)
            Destroy(stick);
        stickVisuals.Clear();
        placedSticks.Clear();

        GridManager.Instance.ResetAllGridPoints();
        GridManager.Instance.ResetAllEdgePlaceholders();
        SFXManager.Instance.PlayWin();
    }

    public void ForceRemoveEdge(Edge e)
    {
        if (!placedSticks.Contains(e)) return;

        placedSticks.Remove(e);

        if (stickVisuals.ContainsKey(e))
        {
            Destroy(stickVisuals[e]);
            stickVisuals.Remove(e);
        }

        // GridPoint’leri güncelle
        UpdateGridPointsForEdge(e);
    }
    
    public void ForceRecalculateSquares()
    {
        completedSquares.Clear();
        foreach (var square in squareVisuals.Values)
        {
            Destroy(square);
        }
        squareVisuals.Clear();
        CheckClosedSquare();
    }

}
