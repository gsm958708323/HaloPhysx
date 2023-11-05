using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;
using System;

public class MoveSystem : IEntitySystem
{
    /// <summary>
    /// 未使用
    /// </summary>
    List<ColliderCompBase> colliderList1 = new();
    /// <summary>
    /// 所属节点管理的entities
    /// </summary>
    List<ColliderCompBase> colliderList2 = new();
    /// <summary>
    /// 碰撞结果
    /// </summary>
    List<CollisionInfo> colliderList3 = new();

    public override void Tick()
    {
        // 动态碰撞和静态碰撞分开
        var moveComps = World.GetComponents<MoveComp>();
        foreach (var item in moveComps)
        {
            var entity = World.GetEntity(item.EntityId);
            if (entity == null)
                continue;

            var moveComp = entity.GetComponent<MoveComp>();
            var transfromComp = entity.GetComponent<TransformComp>();
            if (moveComp != null && transfromComp != null && moveComp.GetVelocity() != PEVector3.zero)
            {
                var collider1 = FindColliderComp(entity);
                if (collider1 == null)
                    continue;

                colliderList1.Clear();
                colliderList2.Clear();
                colliderList3.Clear();

                // 处理碰撞节点
                var node = Entry.SceneManager.GetEntityBelongNode(entity);
                if (node != null)
                {
                    var entityDict = node.GetEntities();
                    foreach (var other in entityDict)
                    {
                        FindColliderComp(other, colliderList2);
                    }
                }

                foreach (var collider2 in colliderList2)
                {
                    if (collider1 == collider2)
                        continue;
                    CollisionInfo info = new();
                    if (collider1.Intersect(collider2, ref info))
                    {
                        Debugger.Log($"发生碰撞：{collider1.ColliderType} {collider2.ColliderType}", LogDomain.Collider);
                        colliderList3.Add(info);
                    }
                }
                DoCollision(collider1, colliderList3);
            }
        }
    }

    private void DoCollision(ColliderCompBase collider, List<CollisionInfo> collisionList)
    {
        var entity = World.GetEntity(collider.EntityId);
        if (entity == null)
            return;

        // 动态碰撞必须有这两个组件
        var moveComp = entity.GetComponent<MoveComp>();
        var transfromComp = entity.GetComponent<TransformComp>();
        if (moveComp == null || transfromComp == null)
            return;

        transfromComp.Translate(moveComp.GetVelocity());
        // 发生碰撞，碰撞位置校正
        if (collisionList.Count == 1)
        {
            // 碰撞一个
            var info = collisionList[0];
            transfromComp.Position = transfromComp.Position + info.Adjust;
        }
        else if (collisionList.Count > 1)
        {
            // 碰撞多个
            PEVector3 adjust = PEVector3.zero;
            foreach (var item in collisionList)
            {
                adjust += item.Adjust;
            }
            transfromComp.Position = transfromComp.Position + adjust;
        }
        collider.Pos = transfromComp.Position;

        // 位置发生改变，更新entity所在的node
        Entry.SceneManager.UpdateEntityNode(entity);
    }

    void FindColliderComp(Entity entity, List<ColliderCompBase> list)
    {
        if (entity == null)
            return;

        var collder = FindColliderComp(entity);
        if (collder != null)
        {
            list.Add(collder);
        }
    }

    ColliderCompBase FindColliderComp(Entity entity)
    {
        if (entity == null)
            return null;

        var collder = entity.GetComponent<SphereColliderComp>();
        if (collder != null)
            return collder;

        var collder2 = entity.GetComponent<BoxColliderComp>();
        if (collder2 != null)
            return collder2;

        return null;
    }
}
