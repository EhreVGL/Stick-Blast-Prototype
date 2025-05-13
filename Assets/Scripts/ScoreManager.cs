using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int totalScore = 0;
    public int TotalScore => totalScore;
    // public Text scoreText;
    public System.Action<int> OnScoreChanged;

    private void Awake(){
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddPoints(int amount){
        totalScore  += amount;

        OnScoreChanged?.Invoke(totalScore);

        // LevelManager Comm
        LevelManager.Instance.OnScoreChanged(totalScore);
    }

    public void ResetScore()
    {
        totalScore = 0;
        HeaderUIManager.Instance.UpdateScoreUI(totalScore);
    }

}
