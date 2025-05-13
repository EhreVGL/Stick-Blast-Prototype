using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShapeData
{
    public string shapeName;
    public List<ShapeRotation> rotations;
}

[System.Serializable]
public class ShapeRotation
{
    public List<RowBool> horizontalEdges;
    public List<RowBool> verticalEdges;
}

[System.Serializable]
public class RowBool
{
    public bool[] values = new bool[2];
}
