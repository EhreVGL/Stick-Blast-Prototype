using System.Collections.Generic;
using UnityEngine;

public class UndoData
{
    public ShapeData shape;
    public int rotationIndex;
    public Vector2Int basePosition;
    public List<Edge> placedEdges;

    public UndoData(ShapeData s, int rot, Vector2Int basePos, List<Edge> edges)
    {
        shape = s;
        rotationIndex = rot;
        basePosition = basePos;
        placedEdges = edges;
    }
}
