using System.Collections.Generic;
using UnityEngine;

public static class GridPlacementValidator
{
    public static bool TryPlaceShape(ShapeData shape, int rotationIndex, Vector2Int baseGridPos, bool checkOnly = false)
    {
        ShapeRotation rot = shape.rotations[rotationIndex];

        List<Edge> edgesToPlace = new List<Edge>();

        // Control
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 2; x++)
            {
                Vector2Int cell = new Vector2Int(baseGridPos.x + x, baseGridPos.y + y);

                if (rot.horizontalEdges[y].values[x])
                {
                    Edge e = new Edge(cell, new Vector2Int(cell.x + 1, cell.y));
                    if (!GridManager.Instance.IsValidEdge(e) || StickPlacementManager.Instance.HasStick(e))
                        return false;

                    edgesToPlace.Add(e);
                }

                if (rot.verticalEdges[y].values[x])
                {
                    Edge e = new Edge(cell, new Vector2Int(cell.x, cell.y + 1));
                    if (!GridManager.Instance.IsValidEdge(e) || StickPlacementManager.Instance.HasStick(e))
                        return false;

                    edgesToPlace.Add(e);
                }
            }
        }

        if (checkOnly) return true;

        // Place
        foreach (Edge e in edgesToPlace)
        {
            StickPlacementManager.Instance.PlaceStickDirectly(e);
        }

        // Kare kontrol
        StickPlacementManager.Instance.CheckClosedSquare();

        // UNDO kaydı
        UndoManager.Instance.RegisterMove(shape, rotationIndex, baseGridPos, edgesToPlace);

        return true;
    }
}
