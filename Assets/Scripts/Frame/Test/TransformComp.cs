using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using System;

public class TransformComp : IComponent
{
    public Vector3 Position;
    public Quaternion Rotation;

    public void Translate(Vector3 Velocity)
    {
        Position += Velocity;
        // Debugger.Log($"{EntityId} 位置更新：{Position}");
    }
}
