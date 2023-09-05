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

    public bool Intersect(ColliderCompBase collider)
    {
        bool result = false;
        if (ColliderType == ColliderType.Box)
        {
            result = IntersectBox(collider);
        }
        else if (ColliderType == ColliderType.Sphere)
        {
            result = IntersectSphere(collider);
        }

        return result;
    }

    protected virtual bool IntersectSphere(ColliderCompBase collider)
    {
        return false;
    }

    protected virtual bool IntersectBox(ColliderCompBase collider)
    {
        return false;
    }
}
