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
    /// 子节点
    /// </summary>
    private List<Node> childs;
    /// <summary>
    /// 当前管理的entity
    /// </summary>
    private List<Entity> entities;

    public Node(Bounds bounds, int depth, Tree root)
    {
        this.Bounds = bounds;
        this.depth = depth;
        this.root = root;
        this.entities = new();
        this.childs = new();
    }

    /// <summary>
    /// 添加此节点管理的entity对象
    /// </summary>
    /// <param name="entity"></param>
    public void AddEntity(Entity entity)
    {
        // 定义子节点的管理范围
        if (depth < root.MaxDepth && childs.Count == 0)
        {
            CreateChild();
        }

        // 添加子节点的管理物体
        int hasCount = 0;
        Node targetNode = null;
        if (childs.Count > 0)
        {
            foreach (var node in childs)
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
            entities.Add(entity);
        }
    }

    /// <summary>
    /// 定义子节点的管理范围
    /// </summary>
    private void CreateChild()
    {
        // 构建坐标：左下 左上 右下 右上 (-1,-1) (-1,1) (1,-1) (1,1)
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                var curSize = Bounds.size;
                var center = new PEVector3(new PEInt(curSize.x / 4 * i), 0, new PEInt(curSize.z / 4 * j));
                var size = new PEVector3(new PEInt(curSize.x / 2), 0, new PEInt(curSize.z / 2));
                var bounds = new Bounds(Bounds.center + center.ConvertViewVector3(), size.ConvertViewVector3());
                var node = new Node(bounds, depth + 1, root);
                childs.Add(node);
            }
        }
    }

    public void OnDraw()
    {
        if (entities.Count != 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size - Vector3.one * 0.1f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Bounds.center, Bounds.size - Vector3.one * 0.1f);
        }

        foreach (var item in childs)
        {
            item.OnDraw();
        }
    }
}
