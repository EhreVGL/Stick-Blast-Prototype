using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelColorTheme", menuName = "StickBlast/Game/Level Color Theme")]
public class LevelColorTheme : ScriptableObject
{
    public List<ThemeData> levelThemes;

    public ThemeData GetThemeForLevel(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, levelThemes.Count - 1);
        return levelThemes[index];
    }
}
