using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class SphereColliderComp : ColliderCompBase
{
    protected override bool IntersectBox(ColliderCompBase collider)
    {
        return false;
    }
}
