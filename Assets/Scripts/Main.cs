using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEMath;
using System;
using Frame;
using Unity.VisualScripting;

public class Main : MonoBehaviour
{
    public Transform EnvTransform;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        InitSimulation();
    }

    void InitSimulation()
    {
        // 添加模拟器
        var sim = new Simulation(Define.Client_Simulation);
        Entry.SimulationManager.AddSimulation(sim);

        // 处理行为
        sim.AddBehaviour<InputBehaviour>();
        var behaviour = sim.AddBehaviour<EntityBehaviour>();
        behaviour.AddSystem<MoveSystem>();

        // 添加全局组件
        sim.GetWorld().AddComponent<EnvironmentComp>();

        // 添加玩家实体
        var entity = sim.GetWorld().AddEntity(Define.Player_EntityId);
        entity.AddComponent<MoveComp>();
        var transComp = entity.AddComponent<TransformComp>();
        var collider = entity.AddComponent<SphereColliderComp>();
        collider.InitByEngineCollider(Player.GetComponent<CapsuleCollider>(), transComp);

        var render = Player.AddComponent<MoveRender>();
        render.SetEntityId(Define.Player_EntityId);

        Entry.SceneManager.InitEnv(EnvTransform);
    }
}
