﻿using System;
using System.Collections.Generic;
using Frame;
using PEMath;
using UnityEngine;

/// <summary>
/// 四叉树场景管理
/// </summary>
public class SceneManager : IManager
{
    public List<Entity> Entities = new();
    public Bounds Bounds;
    Tree tree;

    public void InitEnv(Transform EnvTransform)
    {
        InitTree(EnvTransform);
        InitCollider(EnvTransform);
    }

    private void InitTree(Transform EnvTransform)
    {
        var bound = EnvTransform.GetComponent<BoxCollider>().bounds;
        tree = new Tree(bound);
    }

    private void InitCollider(Transform EnvTransform)
    {
        var simulation = Entry.SimulationManager.GetSimulation(Define.Client_Simulation);
        var boxColliders = EnvTransform.GetComponentsInChildren<BoxCollider>();
        foreach (var item in boxColliders)
        {
            if (!item.gameObject.activeInHierarchy)
                continue;

            var entity = simulation.GetWorld().AddEntity(Guid.NewGuid());
            var comp = entity.AddComponent<BoxColliderComp>();
            var transComp = entity.AddComponent<TransformComp>();
            comp.InitByEngineCollider(item, transComp);
            AddEntity(entity);
        }

        var sphereColliders = EnvTransform.GetComponentsInChildren<CapsuleCollider>();
        foreach (var item in sphereColliders)
        {
            if (!item.gameObject.activeInHierarchy)
                continue;
            var entity = simulation.GetWorld().AddEntity(Guid.NewGuid());
            var comp = entity.AddComponent<SphereColliderComp>();
            var transComp = entity.AddComponent<TransformComp>();
            comp.InitByEngineCollider(item, transComp);
            AddEntity(entity);
        }
    }

    public void AddEntity(Entity entity)
    {
        tree.AddEntity(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        tree.RemoveEntity(entity);
    }

    public void UpdateEntityNode(Entity entity)
    {
        tree.UpdateEntityNode(entity);
    }

    public Node GetEntityBelongNode(Entity entity)
    {
        return tree.GetEntityBelongNode(entity);
    }

    public void OnDraw()
    {
        tree.OnDraw();
    }
}