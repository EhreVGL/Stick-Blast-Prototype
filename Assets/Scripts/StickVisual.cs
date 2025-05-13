using UnityEngine;

public class StickVisual : MonoBehaviour, IThemeColorUpdatable
{
    private void Start() => UpdateThemeColor();
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
        var theme = LevelColorManager.Instance.Theme.GetThemeForLevel(LevelManager.Instance.CurrentLevel);
        GetComponent<SpriteRenderer>().color = theme.stickColor;
    }
}
