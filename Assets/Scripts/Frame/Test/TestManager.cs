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

        // 创建模拟器
        var sim = new Simulation(Define.Client_Simulation);
        sim.AddBehaviour<InputBehaviour>();
        var behaviour = sim.AddBehaviour<EntityBehaviour>();
        behaviour.AddSystem<MoveSystem>();
        Entry.SimulationManager.AddSimulation(sim);

        // 创建实体
        var entity = sim.GetWorld().AddEntity(Define.Player_EntityId);
        entity.AddComponent<MoveComp>();
        entity.AddComponent<TransformComp>();

        // 绑定表现层
        var player = GameObject.Find("Player");
        var render = player.AddComponent<MoveRender>();
        render.SetEntityId(Define.Player_EntityId);
    }
}
