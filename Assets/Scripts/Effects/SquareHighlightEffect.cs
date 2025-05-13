using UnityEngine;
using DG.Tweening;

public class SquareHighlightEffect : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Glow Efekti")]
    public GameObject glowPrefab; // Glow sprite prefab’ı

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartFlashEffect()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        if (glowPrefab != null)
        {
            GameObject glow = Instantiate(glowPrefab, transform.position, Quaternion.identity, transform);
            SpriteRenderer glowSR = glow.GetComponent<SpriteRenderer>();

            glowSR.color = new Color(1f, 1f, 1f, 0.6f);
            glow.transform.localScale = Vector3.zero;

            Sequence glowSeq = DOTween.Sequence();
            glowSeq.Append(glow.transform.DOScale(1.5f, 0.4f).SetEase(Ease.OutQuad));
            glowSeq.Join(glowSR.DOFade(0f, 0.4f).SetEase(Ease.InQuad));
            glowSeq.OnComplete(() => Destroy(glow));
        }

        Transform targetChild = transform.GetChild(0);
        if (targetChild != null)
        {
            Destroy(targetChild.gameObject, 1.3f);
        }
    }
}
