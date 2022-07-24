using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools;

public class TestPosition : BaseObject
{
    public float positionX;
    public float eulerX;

    protected override void OnValidate()
    {
        base.OnValidate();

        PosX = positionX;
        EulerX = eulerX;
    }
}
