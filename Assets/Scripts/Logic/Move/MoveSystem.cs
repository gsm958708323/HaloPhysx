using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;
using System;

public class MoveSystem : IEntitySystem
{
    /// <summary>
    /// 动态碰撞
    /// </summary>
    List<ColliderCompBase> colliderList1 = new();
    /// <summary>
    /// 静态碰撞
    /// </summary>
    List<ColliderCompBase> colliderList2 = new();
    /// <summary>
    /// 碰撞结果
    /// </summary>
    List<CollisionInfo> colliderList3 = new();

    public override void Tick()
    {
        // // 动态碰撞和静态碰撞分开
        // colliderList1.Clear();
        // colliderList2.Clear();

        // var entityList = World.GetEntities();
        // foreach (var entity in entityList)
        // {
        //     var moveComp = entity.GetComponent<MoveComp>();
        //     if (moveComp != null)
        //     {
        //         FindColliderComp(entity, colliderList1);
        //     }
        //     else
        //     {
        //         FindColliderComp(entity, colliderList2);
        //     }
        // }

        // foreach (var collider1 in colliderList1)
        // {
        //     colliderList3.Clear();
        //     foreach (var collider2 in colliderList2)
        //     {
        //         if (collider1 == collider2) continue;
        //         CollisionInfo info = new();
        //         if (collider1.Intersect(collider2, ref info))
        //         {
        //             Debugger.Log($"发生碰撞：{collider1.ColliderType} {collider2.ColliderType}", LogDomain.Collider);
        //             colliderList3.Add(info);
        //         }
        //     }

        //     DoCollision(collider1, colliderList3);
        // }


        var moveComps = World.GetComponents<MoveComp>();
        foreach (var item in moveComps)
        {
            var entity = World.GetEntity(item.EntityId);
            if (entity == null)
                continue;

            // 处理移动
            var moveComp = entity.GetComponent<MoveComp>();
            var transfromComp = entity.GetComponent<TransformComp>();
            if (moveComp != null || transfromComp != null)
            {
                transfromComp.Translate(moveComp.GetVelocity());

                if (Define.TestValue == 1)
                {
                    // 位置发生改变，更新entity所在的node
                    Entry.SceneManager.UpdateEntityNode(entity);
                }
            }

            // var node = Entry.SceneManager.GetEntityBelongNode(entity);
            // if (node == null)
            //     continue;

            // var entityDict = node.GetEntities();
            // Debugger.Log(entityDict.Count.ToString());

            // foreach (var other in entityDict)
            // {
            //     if (entity == other)
            //         continue;
            // }
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

        var collder = entity.GetComponent<SphereColliderComp>();
        if (collder != null)
            list.Add(collder);

        var collder2 = entity.GetComponent<BoxColliderComp>();
        if (collder2 != null)
            list.Add(collder2);
    }
}
