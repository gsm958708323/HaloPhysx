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
        InitEnv();
    }

    void InitSimulation()
    {
        // 添加模拟器
        var sim = new Simulation(Define.Client_Simulation);

        // 处理行为
        sim.AddBehaviour<InputBehaviour>();
        var behaviour = sim.AddBehaviour<EntityBehaviour>();
        behaviour.AddSystem<MoveSystem>();
        behaviour.AddSystem<CollisionSystem>();

        // 添加全局组件
        sim.GetWorld().AddComponent<EnvironmentComp>();
        Entry.SimulationManager.AddSimulation(sim);

        // 添加玩家实体
        var entity = sim.GetWorld().AddEntity(Define.Player_EntityId);
        entity.AddComponent<MoveComp>();
        entity.AddComponent<TransformComp>();
        var collider = entity.AddComponent<SphereColliderComp>();
        collider.InitByEngineCollider(Player.GetComponent<CapsuleCollider>());

        var render = Player.AddComponent<MoveRender>();
        render.SetEntityId(Define.Player_EntityId);
    }

    void InitEnv()
    {
        var simulation = Entry.SimulationManager.GetSimulation(Define.Client_Simulation);
        var boxColliders = EnvTransform.GetComponentsInChildren<BoxCollider>();
        foreach (var item in boxColliders)
        {
            if (!item.gameObject.activeInHierarchy)
                continue;

            var entity = simulation.GetWorld().AddEntity(Guid.NewGuid());
            var comp = entity.AddComponent<BoxColliderComp>();
            comp.InitByEngineCollider(item);
        }

        var sphereColliders = EnvTransform.GetComponentsInChildren<CapsuleCollider>();
        foreach (var item in sphereColliders)
        {
            if (!item.gameObject.activeInHierarchy)
                continue;
            var entity = simulation.GetWorld().AddEntity(Guid.NewGuid());
            var comp = entity.AddComponent<SphereColliderComp>();
            comp.InitByEngineCollider(item);
        }
    }
}
