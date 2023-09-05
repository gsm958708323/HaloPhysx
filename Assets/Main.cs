using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEMath;
using System;
using Physx;
using Frame;

public class Main : MonoBehaviour
{
    public Transform Env;
    public Transform Player;

    EnvCollider envCollider;
    public PlayerLogic PlayerLogic;


    // Start is called before the first frame update
    void Start()
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

        // 添加实体组件
        var entity = sim.GetWorld().AddEntity(Define.Player_EntityId);
        entity.AddComponent<MoveComp>();
        entity.AddComponent<TransformComp>();
    }

    private void InitPlayer()
    {
        var cfg = new CylinderConfig
        {
            Name = "Player",
            Pos = new PEVector3(Player.position),
            Radius = (PEInt)(Player.localScale.x / 2),
        };

        PlayerLogic.Init();
        PlayerLogic.Enter(cfg);
    }

    private void InitEnv()
    {
        // todo 改成json配置
        List<ColliderConfigBase> colliderCfgList = CreateEnvColliderCfgList();
        envCollider = new EnvCollider();
        envCollider.Init();
        envCollider.Enter(colliderCfgList);
    }

    private List<ColliderConfigBase> CreateEnvColliderCfgList()
    {
        var cfgList = new List<ColliderConfigBase>();
        var boxColliders = Env.GetComponentsInChildren<BoxCollider>();
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

        CapsuleCollider[] cylinderArr = Env.GetComponentsInChildren<CapsuleCollider>();
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

/*
var driver = Entry.Instance.GetManager<UpdateDriver>()
updateDriver.AddUpdater<Rener>()
updateDriver.AddUpdater<Logic>()
// 渲染和表现分离
public class Logic:IUpdate{
    void Init()
    void Enter()
    void Exit()

    void Update(deltaTime)
    {
        if(cacheTime > frameCost)
        {
            Tick()
        }
    }

    void Tick()
    {
        playerLogic.Tick()
    }

}

public class Render:Mono, IUpdate{
    void Init()
    void Enter()
    void Exit()

    void Update(deltaTime)
    {
        playerRender.Update()
    }
}

// 逻辑和表现通讯


*/