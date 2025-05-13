using UnityEngine;

public class ShapeSlotManager : MonoBehaviour
{
    public static ShapeSlotManager Instance;
    public ShapeSlot[] slots;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public void NotifySlotCleared(ShapeSlot slot){
        if(AllSlotsCleared()){
            SpawnNewSet();
        }
    }

    private bool AllSlotsCleared(){
        foreach(var slot in slots){
            if(slot.IsOccupied()) return false;
        }
        return true;
    }

    public void SpawnNewSet(){
        foreach (var slot in slots)
        {
            slot.ClearShape();
        }
        
        foreach(var slot in slots){
            slot.SpawnNewShape();
        }
    }

    public void CheckForGameOver()
    {
        foreach (var slot in slots)
        {
            if (slot.HasValidMove())
            {
                return; // En az bir hamle varsa devam
            }
        }

        GameManager.Instance.GameOver(); // Oyunu bitir
    }

    public ShapeSlot GetFirstEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }
        return null;
    }

    private void Start()
    {
        SpawnNewSet();
    }

    public void ClearAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.ClearShape(); // ✅ Bu her bir slot’taki şekli siler
        }
    }
}
