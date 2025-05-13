using UnityEngine;

public class TapEffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tapEffectPrefab;
    public Camera mainCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;

            Instantiate(tapEffectPrefab, worldPos, Quaternion.identity);
        }
    }
}
