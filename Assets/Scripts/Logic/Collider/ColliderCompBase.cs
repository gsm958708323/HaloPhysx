using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using System;
using PEMath;

public enum ColliderType
{
    Box,
    Sphere,
    Cylinder,
}

public class CollisionInfo
{
    /// <summary>
    /// 发生碰撞的组件
    /// </summary>
    public ColliderCompBase Colider;
    /// <summary>
    /// 碰撞位置校正
    /// </summary>
    public PEVector3 Adjust;
}

public abstract class ColliderCompBase : IComponent
{
    public PEVector3 Pos { get; internal set; }
    public string Name { get; internal set; }

    public ColliderType ColliderType;

    public virtual void InitByEngineCollider(UnityEngine.Collider unityCollider)
    {
    }

    public bool Intersect(ColliderCompBase collider, ref CollisionInfo info)
    {
        bool result = false;

        var targetType = collider.ColliderType;
        if (targetType == ColliderType.Box)
        {
            result = IntersectBox(collider as BoxColliderComp, ref info);
        }
        else if (targetType == ColliderType.Sphere)
        {
            result = IntersectSphere(collider as SphereColliderComp, ref info);
        }

        return result;
    }

    protected virtual bool IntersectSphere(SphereColliderComp collider, ref CollisionInfo info)
    {
        return false;
    }

    protected virtual bool IntersectBox(BoxColliderComp collider, ref CollisionInfo info)
    {
        return false;
    }
}
