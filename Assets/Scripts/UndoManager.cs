using System.Collections.Generic;
using UnityEngine;

public class UndoManager : MonoBehaviour
{
    public static UndoManager Instance;

    private Stack<UndoData> undoStack = new Stack<UndoData>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterMove(ShapeData shape, int rotation, Vector2Int basePos, List<Edge> placedEdges)
    {
        undoStack.Push(new UndoData(shape, rotation, basePos, placedEdges));
    }

    public void TryUndo()
    {
        if (undoStack.Count == 0)
        {
            return;
        }

        UndoData data = undoStack.Pop();

        // Çubukları sil
        foreach (Edge e in data.placedEdges)
        {
            StickPlacementManager.Instance.ForceRemoveEdge(e);
        }

        StickPlacementManager.Instance.ForceRecalculateSquares();

        ShapeSpawner.Instance.SpawnSpecificShape(data.shape, data.rotationIndex);
    }
}
