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

    /// <summary>
    /// 获取entity所属节点
    /// </summary>
    Dictionary<Entity, Node> allEntity2Node;

    public Tree(Bounds bounds)
    {
        Bounds = bounds;
        MaxDepth = 5;
        ChildCount = 4;
        root = new Node(bounds, 0, this, null);
        allEntity2Node = new();
    }

    /// <summary>
    /// 重新计算entity的所属节点
    /// </summary>
    /// <param name="entity"></param>
    public void UpdateEntityNode(Entity entity)
    {
        // todo 超出节点范围才移除

        // 节点删除此entity，若此节点管理数量为0，父节点清除此节点的引用
        if (allEntity2Node.TryGetValue(entity, out Node oldNode))
        {
            oldNode.RemoveEntity(entity);
            allEntity2Node.Remove(entity);
        }
        AddEntity(entity);
    }

    public void AddEntityNode(Entity entity, Node node)
    {
        if (allEntity2Node.ContainsKey(entity))
        {
            Debugger.LogError($"[四叉树] entity已经存在字典中", LogDomain.Quadtree);
            return;
        }
        allEntity2Node[entity] = node;
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

/*
todo：
1. 四叉树只管理静态场景，动态物体不管理
2. 位置确定所属节点，改成边界接触
*/
