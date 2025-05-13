using UnityEngine;
using UnityEngine.Rendering;

public class GridPoint : MonoBehaviour, IThemeColorUpdatable
{
    public Vector2Int coordinates;
    public bool isConnected = false;

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
        Color c = isConnected ? theme.gridPointActiveColor : theme.gridPointDefaultColor;
        GetComponent<SpriteRenderer>().color = c;
    }

    public void UpdateVisual(bool connected)
    {
        isConnected = connected;
        UpdateThemeColor();
    }

    private void OnMouseDown(){
        if (PowerupManager.Instance.activePowerup == PowerupType.None) return;
        PowerupManager.Instance.UsePowerupOnCell(coordinates);
    }
    
    public void Highlight()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow; // örnek renk
    }

    public void Unhighlight()
    {
        GetComponent<SpriteRenderer>().color = Color.white; // varsayılan renk
    }
}
