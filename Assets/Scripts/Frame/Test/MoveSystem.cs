using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class MoveSystem : IEntitySystem
{
    public override void Tick()
    {
        var entityList = World.GetEntities();
        foreach (var entity in entityList)
        {
            var moveComp = entity.GetComponent<MoveComp>();
            var transfromComp = entity.GetComponent<TransformComp>();
            if (moveComp != null && transfromComp != null)
            {
                transfromComp.Translate(moveComp.GetVelocity());
            }
        }
    }
}
