using UnityEngine;

public class ShapeSlot : MonoBehaviour
{
    public ShapeSpawner spawner;
    public ShapeSlotManager manager;

    private GameObject currentShapeObj;
    private bool isOccupied = false;
    private GameObject currentShape;

    public void SpawnNewShape()
    {
        currentShapeObj = spawner.SpawnShape(transform.position, this);
        isOccupied = true;
    }

    public void ClearShape()
    {
        isOccupied = false;
        if (currentShape != null)
        {
            Destroy(currentShape);
            currentShape = null;
            isOccupied = false;
        }
    }

    public bool HasValidMove()
    {
        if (currentShapeObj == null) return false;

        ShapeDragHandler drag = currentShapeObj.GetComponent<ShapeDragHandler>();
        if (drag == null) return false;

        ShapeData shape = drag.shapeData;
        int rotationIndex = drag.rotationIndex;

        for (int x = 0; x < GridManager.Instance.gridSize - 1; x++)
        {
            for (int y = 0; y < GridManager.Instance.gridSize - 1; y++)
            {
                Vector2Int basePos = new Vector2Int(x, y);
                if (GridPlacementValidator.TryPlaceShape(shape, rotationIndex, basePos, checkOnly: true))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    public bool IsEmpty()
    {
        return currentShape == null;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void AssignShape(GameObject shape)
    {
        currentShape = shape;
        isOccupied = true;
        
        if (shape != null)
            shape.transform.SetParent(this.transform, true);
    }

}
