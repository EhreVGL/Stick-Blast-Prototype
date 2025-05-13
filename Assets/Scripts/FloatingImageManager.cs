using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class FloatingImageManager : MonoBehaviour
{
    public static FloatingImageManager Instance;
    public GameObject imagePrefab; // UI > Image prefab (RectTransform + Image)
    public ComboSpriteSet spriteSet;
    public Canvas canvas;
    public GameObject comboEffectBrillantPrefab;
    public GameObject comboEffectRipplesPrefab;
    private void Awake()
    {
        Instance = this;
    }

    public void ShowLineClearImage(Vector3 worldPos)
    {
        GameObject go = Instantiate(imagePrefab, canvas.transform);
        RectTransform rt = go.GetComponent<RectTransform>();

        rt.anchoredPosition = WorldToCanvasPosition(worldPos);
        rt.localScale = Vector3.one * 0.5f; 

        Image img = go.GetComponent<Image>();
        img.sprite = spriteSet.lineClearSprite;
        img.SetNativeSize();

        AnimateFloatingImage(rt, img);
    }

    public void ShowComboImage(Vector3 worldPos, int comboLevel)
    {
        GameObject comboGo = Instantiate(imagePrefab, canvas.transform);
        RectTransform comboRT = comboGo.GetComponent<RectTransform>();
        comboRT.anchoredPosition = WorldToCanvasPosition(worldPos);
        comboRT.localScale = Vector3.one * 0.3f;

        Image comboImg = comboGo.GetComponent<Image>();
        comboImg.sprite = spriteSet.comboText;
        comboImg.SetNativeSize();

        AnimateFloatingImage(comboRT, comboImg);

        Vector2 basePos = comboRT.anchoredPosition;
        float xOffset = 255f;

        string digits = comboLevel.ToString();
        for (int i = 0; i < digits.Length; i++)
        {
            int digit = int.Parse(digits[i].ToString());
            Sprite digitSprite = spriteSet.digits[digit];

            GameObject digitGo = Instantiate(imagePrefab, canvas.transform);
            RectTransform digitRT = digitGo.GetComponent<RectTransform>();

            digitRT.anchoredPosition = basePos + new Vector2(xOffset, 0);
            digitRT.localScale = Vector3.one * 0.3f;

            Image digitImg = digitGo.GetComponent<Image>();
            digitImg.sprite = digitSprite;
            digitImg.SetNativeSize();

            AnimateFloatingImage(digitRT, digitImg);

            Vector3 digitWorldPos = Camera.main.ScreenToWorldPoint(
                RectTransformUtility.PixelAdjustPoint(digitRT.anchoredPosition, digitRT, canvas)
            );
            digitWorldPos.z = 0f;

            Transform parent = digitGo.transform;

            if (comboEffectBrillantPrefab != null)
            {
                Vector3 offset = new Vector3(0f, 0f, 0f);
                GameObject fxLeft = Instantiate(comboEffectBrillantPrefab, offset, Quaternion.identity, parent);
                fxLeft.transform.localPosition = offset;
                Destroy(fxLeft, 2f);
            }

            if (comboEffectRipplesPrefab != null)
            {
                Vector3 offset = new Vector3(0f, 0f, 0f);
                GameObject fxRight = Instantiate(comboEffectRipplesPrefab, offset, Quaternion.identity, parent);
                fxRight.transform.localPosition = offset;
                Destroy(fxRight, 2f);
            }
        }
    }

    private Vector2 WorldToCanvasPosition(Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            screenPos,
            Camera.main,
            out Vector2 localPos
        );
        return localPos;
    }

    private void AnimateFloatingImage(RectTransform rt, Image img)
    {
        float duration = 1.2f;

        rt.DOAnchorPosY(rt.anchoredPosition.y + 80f, duration).SetEase(Ease.OutCubic);

        img.DOFade(0f, duration).SetEase(Ease.InQuad);

        rt.DOPunchScale(Vector3.one * 0.2f, 0.4f, 4, 0.6f);

        Destroy(rt.gameObject, duration);
    }

}
