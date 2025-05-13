using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HeaderUIManager : MonoBehaviour
{
    public static HeaderUIManager Instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Image progressFill;

    private int displayedScore = 0;
    private Tween scoreTween;
    private Tween progressTween;

    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
        UpdateScoreUI(ScoreManager.Instance.TotalScore);
    }

    public void UpdateScoreUI(int newScore)
    {
        int currentLevel = LevelManager.Instance.CurrentLevel;
        int nextTarget = LevelManager.Instance.NextLevelScore;

        // Skor artışını animasyonlu göster
        if (scoreTween != null && scoreTween.IsActive()) scoreTween.Kill();

        scoreTween = DOTween.To(() => displayedScore, x => {
            displayedScore = x;
            scoreText.text = displayedScore.ToString();
        }, newScore, 0.5f).SetEase(Ease.OutQuad);

        // Level ve hedef skor güncelle
        levelText.text = $"Lvl.{currentLevel}";
        progressText.text = nextTarget.ToString();

        // Progress bar'ı animasyonla doldur
        float targetProgress = Mathf.Clamp01((float)newScore / nextTarget);

        if (progressTween != null && progressTween.IsActive()) progressTween.Kill();
        progressTween = progressFill.DOFillAmount(targetProgress, 0.5f).SetEase(Ease.OutQuad);
    }
}
