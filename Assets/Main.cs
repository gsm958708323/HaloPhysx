using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEMath;
using System;
using Physx;
using Frame;

public class Main : MonoBehaviour
{
    public Transform EnvTransform;

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

        // // 添加环境实体
        // sim.GetWorld().AddEntity(Define.Env_EntityId);
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
            var trans = item.transform;
            comp.Pos = new PEVector3(trans.position);
            comp.Name = trans.name;
            comp.Axis = new PEVector3[3]{
                    new PEVector3(trans.right),
                    new PEVector3(trans.up),
                    new PEVector3(trans.forward),
            };
        }

        var sphereColliders = EnvTransform.GetComponentsInChildren<CapsuleCollider>();
        foreach (var item in sphereColliders)
        {
            if (!item.gameObject.activeInHierarchy)
                continue;
            var entity = simulation.GetWorld().AddEntity(Guid.NewGuid());
            var comp = entity.AddComponent<SphereColliderComp>();
            comp.Pos = new PEVector3(transform.position);
            comp.Name = transform.gameObject.name;
            comp.Radius = (PEInt)transform.localScale.x / 2;
        }

        Entry.SimulationManager.GetSimulation(Define.Client_Simulation);
    }

    private List<ColliderConfigBase> CreateEnvColliderCfgList()
    {
        var cfgList = new List<ColliderConfigBase>();
        var boxColliders = EnvTransform.GetComponentsInChildren<BoxCollider>();
        foreach (var item in boxColliders)
        {
            if (!item.gameObject.activeInHierarchy)
                continue;
            var trans = item.transform;
            var cfg = new BoxConfig
            {
                Pos = new PEVector3(trans.position),
                Name = trans.name,
                Axis = new PEVector3[3]{
                    new PEVector3(trans.right),
                    new PEVector3(trans.up),
                    new PEVector3(trans.forward),
                },
            };
            cfgList.Add(cfg);
        }

        CapsuleCollider[] cylinderArr = EnvTransform.GetComponentsInChildren<CapsuleCollider>();
        foreach (var item in cylinderArr)
        {
            var cfg = new CylinderConfig
            {
                Pos = new PEVector3(transform.position),
                Name = transform.gameObject.name,
                Radius = (PEInt)transform.localScale.x / 2,
            };
            cfgList.Add(cfg);
        }

        return cfgList;
    }
}
