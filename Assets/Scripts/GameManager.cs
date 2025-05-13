using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        SFXManager.Instance.PlayFail();
        LevelEndUIManager.Instance.ShowPanel(
            ScoreManager.Instance.TotalScore,
            GameStatsTracker.Instance.ShapesUsed,
            GameStatsTracker.Instance.TotalSquares,
            GameStatsTracker.Instance.TotalLineClears,
            LevelEndUIManager.EndPanelMode.GameOver
        );
    }
}
