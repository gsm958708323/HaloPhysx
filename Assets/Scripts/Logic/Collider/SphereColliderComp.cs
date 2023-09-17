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

    public SphereColliderComp()
    {
        ColliderType = ColliderType.Sphere;
    }

    public override void InitByEngineCollider(Collider unityCollider)
    {
        var collider = unityCollider as CapsuleCollider;
        var trans = collider.transform;
        this.Name = trans.name;
        this.Pos = new PEVector3(trans.position);
        this.Radius = (PEInt)trans.localScale.x / 2;
    }

    protected override bool IntersectBox(BoxColliderComp collider)
    {
        return false;
    }

    protected override bool IntersectSphere(SphereColliderComp collider)
    {
        PEVector3 dis = Pos - collider.Pos;
        // 判断两圆心之间的距离是否小于半径
        if (PEVector3.SqrMagnitude(dis) > (Radius + collider.Radius) * (Radius + collider.Radius))
        {
            return false;
        }
        else
        {
            Debugger.Log($"碰撞", LogDomain.Collider);
            return true;
        }
    }
}
