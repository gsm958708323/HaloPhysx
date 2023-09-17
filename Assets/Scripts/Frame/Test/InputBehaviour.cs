using Frame;
using PEMath;
using UnityEngine;
public class InputBehaviour : IBehaviour
{
    public override void Tick()
    {
        var entity = Simulation.GetWorld().GetEntity(Define.Player_EntityId);
        if (entity == null)
            return;

        var moveComp = entity.GetComponent<MoveComp>();
        var x = (PEInt)Input.GetAxis("Horizontal");
        var z = (PEInt)Input.GetAxis("Vertical");
        var y = (PEInt)moveComp.Dir.y;

        moveComp.Dir.x = x;
        moveComp.Dir.y = y;
        moveComp.Dir.z = z;
    }
}

