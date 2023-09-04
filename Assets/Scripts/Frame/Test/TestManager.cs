using System;
using System.Collections;
using System.Collections.Generic;
using Frame;
using UnityEngine;

public class TestManager : IManager
{
    public override void Init()
    {
        base.Init();

        // Entry.DriverManager.AddDriver<MoveDriver>();

        var sim = new Simulation(1);
        var behaviour = sim.AddBehaviour<EntityBehaviour>();
        behaviour.AddSystem<MoveSystem>();
        Entry.SimulationManager.AddSimulation(sim);

        var entity = sim.GetWorld().AddEntity(Guid.NewGuid());
        entity.AddComponent<MoveComp>();
    }
}
