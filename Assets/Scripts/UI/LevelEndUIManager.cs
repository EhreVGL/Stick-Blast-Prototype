using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUIManager : MonoBehaviour
{
    public static LevelEndUIManager Instance;

    [Header("Panel")]
    public GameObject panel;

    [Header("Text Fields")]
    public TMP_Text totalScoreText;
    public TMP_Text shapesUsedText;
    public TMP_Text squaresCreatedText;
    public TMP_Text linesClearedText;

    [Header("Buttons")]
    public Button mainMenuButton;
    public Button continueButton;
    [Header("Other")]
    public Image title;
    public enum EndPanelMode
    {
        LevelComplete,
        GameOver
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        title.gameObject.SetActive(false);
        panel.SetActive(false);
    }
    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    public void ShowPanel(int totalScore, int shapeCount, int squareCount, int lineCount, EndPanelMode mode)
    {
        totalScoreText.text = $"Toplam Puan: {totalScore}";
        shapesUsedText.text = $"Kullanılan Şekil: {shapeCount}";
        squaresCreatedText.text = $"Yapılan Kare: {squareCount}";
        linesClearedText.text = $"Tamamlanan Satır/Sütun: {lineCount}";

        switch (mode)
        {
            case EndPanelMode.LevelComplete:
                continueButton.GetComponentInChildren<TMP_Text>().text = "Devam Et";
                continueButton.onClick.RemoveAllListeners();
                continueButton.onClick.AddListener(OnContinueClicked);
                break;

            case EndPanelMode.GameOver:
                continueButton.GetComponentInChildren<TMP_Text>().text = "Tekrar Dene";
                continueButton.onClick.RemoveAllListeners();
                continueButton.onClick.AddListener(OnMainMenuClicked);
                break;
        }

        panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    private void OnContinueClicked()
    {
        panel.SetActive(false);

        GameStatsTracker.Instance.ResetStats();
    }

    private void OnMainMenuClicked()
    {
        GameStatsTracker.Instance.ResetStats();
        ScoreManager.Instance.ResetScore();
        LevelManager.Instance.ResetToLevel(1);

        StickPlacementManager.Instance.ResetBoard();
        LevelEndUIManager.Instance.HidePanel();
    }
}
