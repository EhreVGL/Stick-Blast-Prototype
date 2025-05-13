using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int currentLevel = 1;
    private int[] levelThresholds = { 200, 500, 900 };
    public int CurrentLevel => currentLevel;
    public int NextLevelScore
    {
        get
        {
            if (currentLevel - 1 < levelThresholds.Length)
                return levelThresholds[currentLevel - 1];
            else
                return levelThresholds[^1]; // Son seviye eşiğini döndür
        }
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnScoreChanged(int totalScore)
    {
        if (currentLevel - 1 < levelThresholds.Length &&
            totalScore >= levelThresholds[currentLevel - 1])
        {
            currentLevel++;
            AdvanceLevel();
        }
    }

    private void AdvanceLevel()
    {
        StickPlacementManager.Instance.ResetBoard();
        LevelColorManager.Instance.RefreshAllThemeColors();
        ShapeSlotManager.Instance.ClearAllSlots();

        // Stats
        LevelEndUIManager.Instance.ShowPanel(
            ScoreManager.Instance.TotalScore,
            GameStatsTracker.Instance.ShapesUsed,
            GameStatsTracker.Instance.TotalSquares,
            GameStatsTracker.Instance.TotalLineClears,
            LevelEndUIManager.EndPanelMode.LevelComplete
        );
    }

    public void ResetToLevel(int level)
    {
        currentLevel = Mathf.Clamp(level, 1, levelThresholds.Length);
    }
}
