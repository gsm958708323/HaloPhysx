using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;
using PEMath;

public class MoveComp : IComponent
{
    public PEInt Speed = 5;
    public PEVector3 Dir = PEVector3.zero;

    public PEVector3 GetVelocity()
    {
        return Dir * Speed * SimulationManager.FrameRate;
    }
}
