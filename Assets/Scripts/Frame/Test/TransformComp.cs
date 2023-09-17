using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using System;
using PEMath;

public class TransformComp : IComponent
{
    public PEVector3 Position;
    public Quaternion Rotation;

    public void Translate(PEVector3 Velocity)
    {
        Position += Velocity;
        // Debugger.Log($"{EntityId} 位置更新：{Position}");
    }
}
