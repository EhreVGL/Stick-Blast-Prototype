using UnityEngine;

public class EdgePlaceholder : MonoBehaviour, IThemeColorUpdatable
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        LevelColorManager.Instance.Register(this);
        UpdateThemeColor();
    }

    private void OnDisable()
    {
        if (LevelColorManager.Instance != null)
            LevelColorManager.Instance.Unregister(this);
    }

    public void UpdateThemeColor()
    {
        ThemeData theme = LevelColorManager.Instance.Theme.GetThemeForLevel(LevelManager.Instance.CurrentLevel);
        sr.color = theme.gridPointDefaultColor;
    }
}
