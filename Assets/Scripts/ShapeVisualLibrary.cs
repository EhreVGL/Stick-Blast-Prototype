using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeVisualLibrary", menuName = "StickBlast/ShapeVisualLibrary")]
public class ShapeVisualLibrary : ScriptableObject
{
    public List<ShapeVisualReference> visuals;

    public Sprite GetSprite(string shapeName, int rotationIndex){
        foreach(var item in visuals){
            if(item.shapeName == shapeName) return item.rotationSprites[rotationIndex];
        }

        return null;
    }
}
