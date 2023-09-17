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

    public BoxColliderComp()
    {
        ColliderType = ColliderType.Box;
    }

    public override void InitByEngineCollider(Collider unityCollider)
    {
        var collider = unityCollider as BoxCollider;
        var trans = collider.transform;
        this.Name = trans.name;
        this.Pos = new PEVector3(trans.position);
        this.Axis = new PEVector3[3]{
                    new PEVector3(trans.right),
                    new PEVector3(trans.up),
                    new PEVector3(trans.forward),
                    };
    }

    protected override bool IntersectSphere(SphereColliderComp collider)
    {
        return false;
    }
}