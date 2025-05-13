using UnityEngine;

public static class GridSnapHelper
{
    public static float gridSize = 1f; // Her hücre 1x1 birimlik kare

    public static Vector2Int GetSnappedGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / gridSize);
        int y = Mathf.RoundToInt(worldPosition.y / gridSize);
        return new Vector2Int(x, y);
    }

    public static Vector3 GridToWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * gridSize, gridPos.y * gridSize, 0);
    }
}