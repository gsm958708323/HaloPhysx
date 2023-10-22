using System;
using System.Collections;
using System.Collections.Generic;
using Frame;
using UnityEngine;

public class Tree : INode
{
    private Node root;
    public int MaxDepth;
    public int ChildCount;

    public Bounds Bounds { get; set; }

    public Tree(Bounds bounds)
    {
        Bounds = bounds;
        MaxDepth = 5;
        ChildCount = 4;
        root = new Node(bounds, 0, this);
    }

    public void AddEntity(Entity entity)
    {
        root.AddEntity(entity);
    }

    public void OnDraw()
    {
        root.OnDraw();
    }
}
