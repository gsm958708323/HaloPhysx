using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;

public class BoxColliderComp : ColliderCompBase
{
    public PEVector3 Pos { get; internal set; }
    public string Name { get; internal set; }
    public PEVector3[] Axis { get; internal set; }

    protected override bool IntersectSphere(ColliderCompBase collider)
    {
        return false;
    }
}
