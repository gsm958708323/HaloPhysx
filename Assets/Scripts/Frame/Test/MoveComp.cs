using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;

public class MoveComp : IComponent
{
    public float Speed = 1;
    public Vector3 Dir = Vector3.zero;

    public Vector3 GetVelocity()
    {
        return Dir * Speed;
    }
}
