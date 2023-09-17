using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;

public class SphereColliderComp : ColliderCompBase
{
    public PEVector3 Pos { get; internal set; }
    public string Name { get; internal set; }
    public PEInt Radius { get; internal set; }

    protected override bool IntersectBox(ColliderCompBase collider)
    {
        return false;
    }
}
