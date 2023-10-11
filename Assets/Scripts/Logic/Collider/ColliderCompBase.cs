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

public abstract class ColliderCompBase : IComponent
{
    public PEVector3 Pos { get; internal set; }
    public string Name { get; internal set; }

    public ColliderType ColliderType;

    public virtual void InitByEngineCollider(UnityEngine.Collider unityCollider)
    {
    }

    public bool Intersect(ColliderCompBase collider)
    {
        bool result = false;
        var targetType = collider.ColliderType;
        if (targetType == ColliderType.Box)
        {
            result = IntersectBox(collider as BoxColliderComp);
        }
        else if (targetType == ColliderType.Sphere)
        {
            result = IntersectSphere(collider as SphereColliderComp);
        }

        return result;
    }

    protected virtual bool IntersectSphere(SphereColliderComp collider)
    {
        return false;
    }

    protected virtual bool IntersectBox(BoxColliderComp collider)
    {
        return false;
    }
}
