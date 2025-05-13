using UnityEngine;

public enum PowerupType
{
    None,
    Arrow,    // OK
    Bomb,     // BOMBA
    Undo,     // GERİ AL
    Shuffle   // KARIŞTIR
}

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;

    public PowerupType activePowerup = PowerupType.None;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetActivePowerup(PowerupType powerup)
    {
        activePowerup = powerup;

         switch (powerup)
        {
            case PowerupType.Undo:
                UseUndo();
                activePowerup = PowerupType.None; 
                break;

            case PowerupType.Shuffle:
                UseShuffle();
                activePowerup = PowerupType.None;
                break;

            default:
                break;
        }
    }

    public void UsePowerupOnCell(Vector2Int cell)
    {
        switch (activePowerup)
        {
            case PowerupType.Arrow:
                ExecuteArrow(cell);
                break;

            case PowerupType.Bomb:
                ExecuteBomb(cell);
                break;

            default:
                return;
        }

        activePowerup = PowerupType.None; // tek seferlik kullanım
    }

    private void ExecuteArrow(Vector2Int cell)
    {

        for (int x = 0; x < 5; x++)
            StickPlacementManager.Instance.ClearSquareAt(x, cell.y);

        for (int y = 0; y < 5; y++)
            StickPlacementManager.Instance.ClearSquareAt(cell.x, y);
    }

    private void ExecuteBomb(Vector2Int center)
    {

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                Vector2Int target = new Vector2Int(center.x + dx, center.y + dy);
                StickPlacementManager.Instance.ClearSquareAt(target.x, target.y);
            }
        }
    }

    public void UseUndo()
    {
        UndoManager.Instance.TryUndo(); // Henüz oluşturulacak
    }

    public void UseShuffle()
    {
        ShapeSlotManager.Instance.SpawnNewSet(); // Zaten çalışıyor olmalı
    }


}
