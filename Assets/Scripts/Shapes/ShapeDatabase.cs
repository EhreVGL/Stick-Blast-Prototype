using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeDatabase", menuName = "StickBlast/ShapeDatabase")]
public class ShapeDatabase : ScriptableObject
{
    public List<ShapeData> shapes;
}
