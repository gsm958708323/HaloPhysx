using System.Collections.Generic;
using Frame;

public class CollisionSystem : IEntitySystem
{
    public override void Tick()
    {
        base.Tick();

        // List<ColliderCompBase> colliderList = new();
        // var boxList = World.GetComponents<BoxColliderComp>();
        // if (boxList != null)
        // {
        //     colliderList.AddRange(boxList);
        // }

        // var sphereList = World.GetComponents<SphereColliderComp>();
        // if (sphereList != null)
        // {
        //     colliderList.AddRange(sphereList);
        // }

        // foreach (var collder1 in colliderList)
        // {
        //     foreach (var collider2 in colliderList)
        //     {
        //         if (collder1 == collider2) continue;
        //         if (collder1.Intersect(collider2))
        //         {
        //             Debugger.Log($"发生碰撞：{collder1.ColliderType} {collider2.ColliderType}", LogDomain.Collider);
        //         }
        //         // 修改Collider.Position 还是 修改 transComp.Position
        //     }
        // }
    }
}
