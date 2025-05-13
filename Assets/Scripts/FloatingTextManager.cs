using UnityEngine;
using TMPro;
using DG.Tweening;
public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance;

    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Canvas worldSpaceCanvas; // Screen Space - Camera

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowText(string text, Vector3 worldPosition, Color color)
    {
        // Canvas'a bağlı olarak instantiate et
        GameObject go = Instantiate(floatingTextPrefab, worldSpaceCanvas.transform);
        RectTransform rt = go.GetComponent<RectTransform>();

        Vector2 anchoredPos;
        RectTransform canvasRect = worldSpaceCanvas.GetComponent<RectTransform>();

        // ✅ Ekran pozisyonunu Canvas koordinatına çevir
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Camera.main.WorldToScreenPoint(worldPosition),
            Camera.main,
            out anchoredPos
        );

        rt.anchoredPosition = anchoredPos;

        // ✅ Yazı içeriği ve rengi
        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.color = color;

        float duration = 1.2f;

        rt.DOAnchorPosY(rt.anchoredPosition.y + 80f, duration).SetEase(Ease.OutCubic);
        tmp.DOFade(0f, duration).SetEase(Ease.InQuad);

        Destroy(go, duration);
    }

    public void ShowTextWithPunch(string text, Vector3 worldPosition, Color color)
    {
        GameObject go = Instantiate(floatingTextPrefab, worldSpaceCanvas.transform);
        RectTransform rt = go.GetComponent<RectTransform>();

        Vector2 anchoredPos;
        RectTransform canvasRect = worldSpaceCanvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Camera.main.WorldToScreenPoint(worldPosition),
            Camera.main,
            out anchoredPos
        );

        rt.anchoredPosition = anchoredPos;

        TextMeshProUGUI tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.color = color;

        float duration = 1.2f;

        // Yukarı süzülme ve fade
        rt.DOAnchorPosY(rt.anchoredPosition.y + 80f, duration).SetEase(Ease.OutCubic);
        tmp.DOFade(0f, duration).SetEase(Ease.InQuad);

        // Punch scale (hoplayarak büyüme)
        rt.DOPunchScale(Vector3.one * 0.3f, 0.4f, 6, 0.6f); // bounce efekti

        Destroy(go, duration);
    }

}
