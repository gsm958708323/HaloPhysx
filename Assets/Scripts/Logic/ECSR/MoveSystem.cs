using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class MoveSystem : IEntitySystem
{
    public override void Tick()
    {
        // 动态碰撞和静态碰撞分开
        List<ColliderCompBase> colliderList1 = new();
        List<ColliderCompBase> colliderList2 = new();
        var entityList = World.GetEntities();
        foreach (var entity in entityList)
        {
            var moveComp = entity.GetComponent<MoveComp>();
            if (moveComp != null)
            {
                FindColliderComp(entity, colliderList1);
            }
            else
            {
                FindColliderComp(entity, colliderList2);
            }
        }

        foreach (var collider1 in colliderList1)
        {
            foreach (var collider2 in colliderList2)
            {
                if (collider1 == collider2) continue;

                if (collider1.Intersect(collider2))
                {
                    // 发生碰撞，碰撞位置校正
                    Debugger.Log($"发生碰撞：{collider1.ColliderType} {collider2.ColliderType}", LogDomain.Collider);
                }
                else
                {
                    // 未发生碰撞，更新碰撞体位置信息
                    var entity = World.GetEntity(collider1.EntityId);
                    if (entity != null)
                    {
                        var moveComp = entity.GetComponent<MoveComp>();
                        var transfromComp = entity.GetComponent<TransformComp>();
                        if (moveComp != null && transfromComp != null)
                        {
                            transfromComp.Translate(moveComp.GetVelocity());
                            if (collider1 != null)
                            {
                                collider1.Pos = transfromComp.Position;
                            }
                        }
                    }
                }
            }
        }
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
