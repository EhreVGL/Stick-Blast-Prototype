using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject edgePlaceholderPrefab;
    public int gridSize = 6;
    public float spacing = 1f;
    private Dictionary<Vector2Int, GridPoint> pointLookup = new Dictionary<Vector2Int, GridPoint>();
    private List<SpriteRenderer> edgePlaceholders = new List<SpriteRenderer>();

    private Vector2Int[,] gridPoints;
    public static GridManager Instance;
    public GridPoint GetGridPointAt(Vector2Int position)
    {
        if (pointLookup.TryGetValue(position, out GridPoint point))
        {
            return point;
        }

        return null;
    }

    private void Awake(){
        Instance = this;
        
        GenerateGrid();
        GenerateEdgePlaceholders();
    }
    private void Start()
    {
        CenterCameraOnGrid();
        AdjustCameraZoom();
    }

    private void CenterCameraOnGrid()
    {
        Vector3 gridCenter = new Vector3((gridSize - 1) / 2f, (gridSize - 1) / 2f, -10f);
        Camera.main.transform.position = gridCenter;
    }
    private void AdjustCameraZoom()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        float requiredVerticalSize = (gridSize + 1) / 2f;
        float requiredHorizontalSize = (gridSize + 1) / (2f * aspectRatio);

        Camera.main.orthographicSize = Mathf.Max(requiredVerticalSize, requiredHorizontalSize);
    }

    private void GenerateGrid(){
        gridPoints = new Vector2Int[gridSize, gridSize];

        for(int x = 0; x < gridSize; x++){
            for(int y = 0; y < gridSize; y++){
                Vector3 pos = new Vector3(x * spacing, y * spacing, 0);
                GameObject obj = Instantiate(pointPrefab, pos, Quaternion.identity, this.transform); // Instantiate(pointPrefab, pos, Quaternion.identity, this.transform);
                
                GridPoint gridPoint = obj.GetComponent<GridPoint>();
                Vector2Int coord = new Vector2Int(x, y);
                
                gridPoint.coordinates = coord;
                gridPoint.UpdateVisual(false);

                pointLookup[coord] = gridPoint;
            }
        }
    }

    private void GenerateEdgePlaceholders()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);

                if (x < gridSize - 1)
                {
                    Vector2Int right = new Vector2Int(x + 1, y);
                    Vector3 pointA = new Vector3(cell.x, cell.y, 0);
                    Vector3 pointB = new Vector3(right.x, right.y, 0);
                    Vector3 mid = (pointA + pointB) / 2f;

                    GameObject placeholder = Instantiate(edgePlaceholderPrefab, mid, Quaternion.identity, transform);
                    float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * Mathf.Rad2Deg;
                    placeholder.transform.rotation = Quaternion.Euler(0, 0, angle);

                    float length = Vector3.Distance(pointA, pointB);
                    placeholder.transform.localScale = new Vector3(1f, 0.1f, 1f);

                    var sr = placeholder.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        edgePlaceholders.Add(sr);
                        ThemeData theme = LevelColorManager.Instance.Theme.GetThemeForLevel(LevelManager.Instance.CurrentLevel);
                        sr.color = theme.gridPointDefaultColor;
                    }
                }

                if (y < gridSize - 1)
                {
                    Vector2Int down = new Vector2Int(x, y + 1);
                    Vector3 pointA = new Vector3(cell.x, cell.y, 0);
                    Vector3 pointB = new Vector3(down.x, down.y, 0);
                    Vector3 mid = (pointA + pointB) / 2f;

                    GameObject placeholder = Instantiate(edgePlaceholderPrefab, mid, Quaternion.identity, transform);
                    float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * Mathf.Rad2Deg;
                    placeholder.transform.rotation = Quaternion.Euler(0, 0, angle);

                    float length = Vector3.Distance(pointA, pointB);
                    placeholder.transform.localScale = new Vector3(1f, 0.1f, 1f);

                    var sr = placeholder.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        edgePlaceholders.Add(sr);
                        ThemeData theme = LevelColorManager.Instance.Theme.GetThemeForLevel(LevelManager.Instance.CurrentLevel);
                        sr.color = theme.gridPointDefaultColor;
                    }
                }
            }
        }
    }
    public void ResetAllGridPoints()
    {
        foreach (var point in pointLookup.Values)
        {
            point.UpdateVisual(false);
        }
    }
    public void ResetAllEdgePlaceholders()
    {
        ThemeData theme = LevelColorManager.Instance.Theme.GetThemeForLevel(LevelManager.Instance.CurrentLevel);
        foreach (var sr in edgePlaceholders)
        {
            sr.color = theme.gridPointDefaultColor;
        }
    }
    public bool IsValidEdge(Edge edge)
    {
        return IsValidPoint(edge.A) &&
            IsValidPoint(edge.B) &&
            (edge.A.x == edge.B.x || edge.A.y == edge.B.y) &&
            Mathf.Abs(edge.A.x - edge.B.x) <= 1 &&
            Mathf.Abs(edge.A.y - edge.B.y) <= 1;
    }

    private bool IsValidPoint(Vector2Int p)
    {
        return p.x >= 0 && p.x < gridSize && p.y >= 0 && p.y < gridSize;
    }

}
