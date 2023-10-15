using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;

public class SphereColliderComp : ColliderCompBase
{
    public PEInt Radius { get; internal set; }

    public SphereColliderComp()
    {
        ColliderType = ColliderType.Sphere;
    }

    public override void InitByEngineCollider(Collider unityCollider, TransformComp transformComp = null)
    {
        var collider = unityCollider as CapsuleCollider;
        var trans = collider.transform;
        this.Name = trans.name;
        this.Pos = new PEVector3(trans.position);
        this.Radius = (PEInt)trans.localScale.x / 2;

        base.InitByEngineCollider(unityCollider, transformComp);
    }

    /// <summary>
    /// 圆与矩形：找到圆心O与矩形K最近的点P，判断圆心与矩形的距离OP
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    protected override bool IntersectBox(BoxColliderComp collider, ref CollisionInfo info)
    {
        PEVector3 dir = Pos - collider.Pos;
        // 计算方向向量在矩形内的投影长度
        var disX = PEVector3.Dot(dir, collider.Axis[0]); // 水平方向分量
        var disZ = PEVector3.Dot(dir, collider.Axis[2]); // 竖直方向分量

        // 限制长度在矩形内
        var clampX = PECalc.Clamp(disX, -collider.Size.x, collider.Size.x);
        var clampZ = PECalc.Clamp(disZ, -collider.Size.z, collider.Size.z);

        // 计算矩形轴向对应的向量
        var dirX = clampX * collider.Axis[0];
        var dirZ = clampZ * collider.Axis[2];

        // 计算p点：矩形中心+投影的分量
        var p = collider.Pos + dirX + dirZ;
        var po = Pos - p;
        po.y = 0;
        if (PEVector3.SqrMagnitude(po) > Radius * Radius)
        {
            return false;
        }
        else
        {
            info.Colider = collider;
            info.Adjust = po.normalized * (Radius - po.magnitude);
            return true;
        }
    }

    /// <summary>
    /// 圆与圆：判断两圆心之间的距离是否小于半径
    /// </summary>
    protected override bool IntersectSphere(SphereColliderComp collider, ref CollisionInfo info)
    {
        PEVector3 dis = Pos - collider.Pos;
        if (PEVector3.SqrMagnitude(dis) > (Radius + collider.Radius) * (Radius + collider.Radius))
        {
            return false;
        }
        else
        {
            info.Colider = collider;
            info.Adjust = dis.normalized * (Radius + collider.Radius - dis.magnitude);
            return true;
        }
    }
}
