using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class MoveRender : IRender
{
    protected override void OnUpdate(Entity entity)
    {
        TransformComp transComp = entity.GetComponent<TransformComp>();
        if (transComp != null)
        {
            gameObject.transform.position = transComp.Position;
        }
    }
}
