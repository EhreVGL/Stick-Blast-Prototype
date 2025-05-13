using System.Collections.Generic;
using UnityEngine;

public class LevelColorManager : MonoBehaviour
{
    public static LevelColorManager Instance;

    public LevelColorTheme themes;
    public LevelColorTheme Theme => themes;
    private List<IThemeColorUpdatable> themeListeners = new List<IThemeColorUpdatable>();

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Register(IThemeColorUpdatable obj)
    {
        if (!themeListeners.Contains(obj))
            themeListeners.Add(obj);
    }

    public void Unregister(IThemeColorUpdatable obj)
    {
        themeListeners.Remove(obj);
    }

    public void RefreshAllThemeColors()
    {
        foreach (var updatable in themeListeners)
        {
            updatable.UpdateThemeColor();
        }
    }
}
