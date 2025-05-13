using DG.Tweening;
using UnityEngine;

public class ShakeScreenEffect : MonoBehaviour
{
    public static ShakeScreenEffect Instance;
    [SerializeField] private Camera mainCamera;
    private Vector3 originalPos;
    public bool isActive = false;
    void Awake()
    {
        if(Instance == null) Instance = this;
    }
    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void ShakeScreen(float duration = 0.6f, float strength = 0.3f, int vibrato = 10)
    {
        originalPos = mainCamera.transform.position;
        mainCamera.transform.DOComplete(); // Önceki animasyonu bitir
        mainCamera.transform.DOShakePosition(duration, strength, vibrato)
            .OnComplete(() => mainCamera.transform.position = originalPos);
    }
    void Update()
    {
        if(isActive == true){isActive=false; ShakeScreen();}
    }

}
