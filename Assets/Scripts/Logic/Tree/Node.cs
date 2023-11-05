using System;
using System.Collections;
using System.Collections.Generic;
using Frame;
using PEMath;
using UnityEngine;

public interface INode
{
    Bounds Bounds { get; set; }
    void AddEntity(Entity entity);
    void OnDraw();
}

public enum NodeType
{
    None,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown,
}

public class Node : INode
{
    /// <summary>
    /// 包围盒
    /// </summary>
    public Bounds Bounds { get; set; }
    /// <summary>
    /// 当前节点的深度
    /// </summary>
    private int depth;
    /// <summary>
    /// 根节点
    /// </summary>
    private Tree root;
    /// <summary>
    /// 父节点
    /// </summary>
    private Node parent;
    /// <summary>
    /// 子节点
    /// </summary>
    private Dictionary<NodeType, Node> childDict;
    /// <summary>
    /// 当前管理的entity
    /// </summary>
    private HashSet<Entity> entitySet;

    public Node(Bounds bounds, int depth, Tree root, Node parent)
    {
        this.Bounds = bounds;
        this.depth = depth;
        this.root = root;
        this.parent = parent;
        this.entitySet = new();
        this.childDict = new();
    }

    public HashSet<Entity> GetEntities()
    {
        return entitySet;
    }

    public void RemoveEntity(Entity entity)
    {
        if (entitySet.Contains(entity))
        {
            entitySet.Remove(entity);
        }

        // 节点删除此entity，若此节点管理数量为0，父节点清除此节点的引用
        var node = this;
        while (node != null && node.entitySet.Count == 0)
        {
            var parent = node.parent;
            NodeType targetKey = NodeType.None;
            foreach (var item in parent.childDict)
            {
                if (item.Value == node)
                {
                    targetKey = item.Key;
                    break;
                }
            }
            if (targetKey != NodeType.None)
            {
                parent.childDict.Remove(targetKey);
            }
            node = node.parent;
        }
    }

    /// <summary>
    /// 添加此节点管理的entity对象
    /// </summary>
    /// <param name="entity"></param>
    public void AddEntity(Entity entity)
    {
        // 定义子节点的管理范围
        if (depth < root.MaxDepth && childDict.Count != root.ChildCount)
        {
            CreateChild();
        }

        // 添加子节点的管理物体
        int hasCount = 0;
        Node targetNode = null;
        if (childDict.Count > 0)
        {
            foreach (var node in childDict.Values)
            {
                var trans = entity.GetComponent<TransformComp>();
                if (trans != null && node.Bounds.Contains(trans.Position.ConvertViewVector3()))
                {
                    hasCount += 1;
                    targetNode = node;
                }
            }
        }

        // 如果同一个entity受到两个子节点管理（即边界上），则把它放到当前节点管理
        if (targetNode != null && hasCount == 1)
        {
            targetNode.AddEntity(entity);
        }
        else
        {
            entitySet.Add(entity);
            root.RecordEntityNode(entity, this);
        }
    }

    /// <summary>
    /// 定义子节点的管理范围
    /// </summary>
    private void CreateChild()
    {
        AddChild(NodeType.LeftUp);
        AddChild(NodeType.LeftDown);
        AddChild(NodeType.RightUp);
        AddChild(NodeType.RightDown);
    }

    void AddChild(NodeType nodeType)
    {
        if (childDict.ContainsKey(nodeType))
            return;

        var childSize = Bounds.size / 2;
        var quarter = Bounds.size / 4;
        int i = 0; int j = 0;
        if (nodeType == NodeType.LeftUp)
        {
            i = -1; j = 1;
        }
        else if (nodeType == NodeType.LeftDown)
        {
            i = -1; j = -1;
        }
        else if (nodeType == NodeType.RightUp)
        {
            i = 1; j = 1;
        }
        else if (nodeType == NodeType.RightDown)
        {
            i = 1; j = -1;
        }

        var childCenter = new Vector3(quarter.x * i, 0, quarter.z * j);
        var bounds = new Bounds(new PEVector3(Bounds.center + childCenter).ConvertViewVector3(), new PEVector3(childSize).ConvertViewVector3());
        childDict[nodeType] = new Node(bounds, depth + 1, root, this);
    }

    public void OnDraw()
    {
        if (entitySet.Count != 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size - Vector3.one * 0.1f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size - Vector3.one * 0.1f);
        }

        foreach (var item in childDict.Values)
        {
            item.OnDraw();
        }
    }
}
