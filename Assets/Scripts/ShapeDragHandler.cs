using UnityEngine;

public class ShapeDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 originalPosition;
    public ShapeData shapeData;
    public int rotationIndex;
    private ShapeSlot mySlot;

    public void Initialize(ShapeData data, int rotation, ShapeSlot slot)
    {
        shapeData = data;
        rotationIndex = rotation;
        mySlot = slot;
    }

    void OnMouseDown()
    {
        originalPosition = transform.position;
        offset = transform.position - GetMouseWorldPos();
        SFXManager.Instance.PlayClick();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
        // Ghost
        Vector2Int gridPos = GridSnapHelper.GetSnappedGridPosition(transform.position);
        GhostPreviewManager.Instance.ShowPreview(shapeData, rotationIndex, gridPos);

    }

    void OnMouseUp()
    {
        // Burada grid'e bırakma kontrolü yapılır
        Vector2Int gridPos = GridSnapHelper.GetSnappedGridPosition(transform.position);
        bool valid = GridPlacementValidator.TryPlaceShape(shapeData, rotationIndex, gridPos);

        if(valid)
        {
            GhostPreviewManager.Instance.ClearPreview();
            mySlot.ClearShape();
            ShapeSlotManager.Instance.NotifySlotCleared(mySlot);
            Destroy(gameObject);
    
            ShapeSlotManager.Instance.CheckForGameOver();

            // Stats
            GameStatsTracker.Instance.AddShapeUsed();
        }
        else
        {
            transform.position = originalPosition; // geçersiz → geri dön
            GhostPreviewManager.Instance.ClearPreview();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
