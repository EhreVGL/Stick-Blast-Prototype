using System.Collections.Generic;
using UnityEngine;

public class GhostPreviewManager : MonoBehaviour
{
    public GameObject ghostHorizontalPrefab;
    public GameObject ghostVerticalPrefab;
    private List<GameObject> activeGhosts = new List<GameObject>();
    
    public static GhostPreviewManager Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void ShowPreview(ShapeData shape, int rotationIndex, Vector2Int baseGridPos)
    {
        ClearPreview();

        ShapeRotation rot = shape.rotations[rotationIndex];

        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 2; x++)
            {
                Vector2Int cell = new Vector2Int(baseGridPos.x + x, baseGridPos.y + y);

                Edge horizontal = new Edge(cell, new Vector2Int(cell.x + 1, cell.y));
                Edge vertical = new Edge(cell, new Vector2Int(cell.x, cell.y + 1));

                if (rot.horizontalEdges[y].values[x] && GridManager.Instance.IsValidEdge(horizontal))
                {
                    SpawnGhost(horizontal);
                }

                if (rot.verticalEdges[y].values[x] && GridManager.Instance.IsValidEdge(vertical))
                {
                    SpawnGhost(vertical);
                }
            }
        }
    }

    private void SpawnGhost(Edge edge)
    {
        Vector3 pointA = new Vector3(edge.A.x, edge.A.y, 0);
        Vector3 pointB = new Vector3(edge.B.x, edge.B.y, 0);
        Vector3 mid = (pointA + pointB) / 2f;
        float length = Vector3.Distance(pointA, pointB);

        bool isHorizontal = edge.A.y == edge.B.y;
        GameObject prefab = isHorizontal ? ghostHorizontalPrefab : ghostVerticalPrefab;

        GameObject ghost = Instantiate(prefab, mid, Quaternion.identity);
        ghost.transform.localScale = new Vector3(0.6f, 0.6f, 1f);

        SpriteRenderer sr = ghost.GetComponent<SpriteRenderer>();
        bool isValid = IsEdgeValid(edge);
        sr.color = isValid ? new Color(0f, 1f, 0f, 0.3f) : new Color(1f, 0f, 0f, 0.3f);

        activeGhosts.Add(ghost);
    }


    private bool IsEdgeValid(Edge e)
    {
        return GridManager.Instance.IsValidEdge(e) && !StickPlacementManager.Instance.HasStick(e);
    }

    public void ClearPreview()
    {
        foreach (var obj in activeGhosts)
        {
            if (obj != null)
                Destroy(obj);
        }

        activeGhosts.Clear();
    }
}
