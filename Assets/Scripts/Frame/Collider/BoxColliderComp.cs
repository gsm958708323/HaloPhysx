using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class BoxColliderComp : ColliderCompBase
{
    protected override bool IntersectSphere(ColliderCompBase collider)
    {
        return false;
    }
}
