using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using System;

public enum ColliderType
{
    Box,
    Sphere,
    Cylinder,
}

public abstract class ColliderCompBase : IComponent
{
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
