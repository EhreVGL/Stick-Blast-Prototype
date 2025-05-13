using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NUnit.Framework;

public class FlashEffect : MonoBehaviour
{
    public static FlashEffect Instance;
    public GameObject effectObject;
    public float intensityMin = 1f;
    public float intensityMax = 1.49f;
    public float totalDuration = 1f;
    private Material effectMaterial;
    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    void Start()
    {
        Image img = effectObject.GetComponent<Image>();
        if (img == null)
        {
            return;
        }

        // Materyali kopyalayarak eşsiz yap
        effectMaterial = Instantiate(img.material);
        img.material = effectMaterial;
    }
    public void TriggerFlash()
    {
        float half = totalDuration / 2f;

        effectObject.SetActive(true);

        effectMaterial.SetFloat("_VignetteIntensity", intensityMin);

        effectMaterial
            .DOFloat(intensityMax, "_VignetteIntensity", half)
            .OnComplete(() =>
                effectMaterial
                    .DOFloat(intensityMin, "_VignetteIntensity", half)
                    .OnComplete(() =>
                        effectObject.SetActive(false) 
                    )
            );
    }
}
