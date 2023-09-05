using Frame;
using UnityEngine;
public class InputBehaviour : IBehaviour
{
    public override void Tick()
    {
        var entity = Simulation.GetWorld().GetEntity(Define.Player_EntityId);
        if (entity == null)
            return;

        var moveComp = entity.GetComponent<MoveComp>();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        var y = moveComp.Dir.y;
        moveComp.Dir = new Vector3(x, y, z);
    }
}

