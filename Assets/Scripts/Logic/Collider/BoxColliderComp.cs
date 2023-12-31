using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;

public class BoxColliderComp : ColliderCompBase
{
    /// <summary>
    /// 半径
    /// </summary>
    public PEVector3 Size { get; private set; }
    /// <summary>
    /// 对应x，y，z轴的方向向量
    /// </summary>
    public PEVector3[] Axis { get; internal set; }

    public BoxColliderComp()
    {
        ColliderType = ColliderType.Box;
    }

    public override void InitByEngineCollider(Collider unityCollider, TransformComp transformComp = null)
    {
        var collider = unityCollider as BoxCollider;
        var trans = collider.transform;
        this.Name = trans.name;
        this.Size = new PEVector3(trans.localScale / 2);
        this.Pos = new PEVector3(trans.position);
        this.Axis = new PEVector3[3]{
                    new PEVector3(trans.right),
                    new PEVector3(trans.up),
                    new PEVector3(trans.forward),
                    };
        base.InitByEngineCollider(unityCollider, transformComp);
    }

    protected override bool IntersectSphere(SphereColliderComp collider, ref CollisionInfo info)
    {
        return false;
    }
}
