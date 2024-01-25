
using UnityEngine;
using StardropTools;

public class TestPosition : BaseTransform
{
    public float positionX;
    public float eulerX;

    protected override void OnValidate()
    {
        base.OnValidate();

        PosX = positionX;
        SetEulerX(eulerX);
    }
}
