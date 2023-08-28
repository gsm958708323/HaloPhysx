using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PEMath;
using System;
using Physx;

public class Main : MonoBehaviour
{
    public Transform Env;
    public Transform Player;

    EnvCollider envCollider;
    PlayerCollider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        InitEnv();
        InitPlayer();
    }

    private void InitPlayer()
    {
        var cfg = new ColliderConfig
        {
            Name = "Player",
            Pos = new PEVector3(Player.position),
            Type = ColliderType.Capsule,
            Radius = (PEInt)(Player.localScale.x / 2),
        };

        playerCollider.Init(cfg);
    }

    private void InitEnv()
    {
        // todo 改成json配置
        List<ColliderConfig> colliderCfgList = CreateEnvColliderCfgList();
        envCollider = new EnvCollider();
        envCollider.Init(colliderCfgList);
    }

    private List<ColliderConfig> CreateEnvColliderCfgList()
    {
        var cfgList = new List<ColliderConfig>();
        var boxColliders = Env.GetComponentsInChildren<BoxCollider>();
        foreach (var item in boxColliders)
        {
            if (!item.gameObject.activeInHierarchy)
                continue;
            var trans = item.transform;
            var cfg = new ColliderConfig{
                Pos = new PEVector3(trans.position),
                Name = trans.name,
                Type = ColliderType.Box,
                Axis = new PEVector3[3]{
                    new PEVector3(trans.right),
                    new PEVector3(trans.up),
                    new PEVector3(trans.forward),
                },
            };
            cfgList.Add(cfg);
        }
        
        return cfgList;
    }

    // Update is called once per frame
    void Update()
    {

    }
}