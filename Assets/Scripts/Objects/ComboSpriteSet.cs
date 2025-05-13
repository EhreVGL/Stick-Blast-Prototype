using UnityEngine;

[CreateAssetMenu(fileName = "ComboSpriteSet", menuName = "StickBlast/Game/Combo Sprite Set")]
public class ComboSpriteSet : ScriptableObject
{
    public Sprite comboText;             // "COMBO"
    public Sprite[] digits;              // 0-9 (index = rakam)
    public Sprite lineClearSprite;       // "Line Clear!" sprite
}
