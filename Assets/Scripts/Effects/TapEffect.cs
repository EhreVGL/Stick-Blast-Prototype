using UnityEngine;
using DG.Tweening;

public class TapEffect : MonoBehaviour
{
    void Start()
    {
        // Efektin scale ile büyüyüp kaybolması
        transform.localScale = Vector3.zero;

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(0.3f, 0f).SetEase(Ease.OutBack))
         .Join(GetComponent<SpriteRenderer>().DOFade(0f, 0.5f))
         .AppendCallback(() => Destroy(gameObject));
    }
}
