using UnityEngine;

public class GameStatsTracker : MonoBehaviour
{
    public static GameStatsTracker Instance;

    public int ShapesUsed { get; private set; }
    public int TotalSquares { get; private set; }
    public int TotalLineClears { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ResetStats()
    {
        ShapesUsed = 0;
        TotalSquares = 0;
        TotalLineClears = 0;
    }

    public void AddShapeUsed()
    {
        ShapesUsed++;
    }

    public void AddSquares(int amount)
    {
        TotalSquares += amount;
    }

    public void AddLineClears(int amount)
    {
        TotalLineClears += amount;
    }
}
