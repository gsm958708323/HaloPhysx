using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    /// <summary>
    /// 模拟器管理
    /// </summary>
    public class SimulationManager : IManager
    {
        public static int TargetFrameRate = 60;
        float perFrameCost;
        float cacheTime;
        public int curFrame;
        List<Simulation> simulationList;

        private SimulationManager()
        {
            simulationList = new();
        }

        public void AddSimulation(Simulation sim)
        {
            if (!ExistSimulation(sim))
                simulationList.Add(sim);
        }
        public void RemoveSimulation(Simulation sim)
        {
            if (ExistSimulation(sim))
                simulationList.Remove(sim);
        }
        public void RemoveSimulation(byte id)
        {
            Simulation sim = GetSimulation(id);
            if (sim != null)
                RemoveSimulation(sim);
        }
        public bool ExistSimulation(Simulation sim)
        {
            return simulationList.Contains(sim);
        }
        public Simulation GetSimulation(byte id)
        {
            for (int i = 0; i < simulationList.Count; ++i)
            {
                if (simulationList[i].GetSimulationId() == id) return simulationList[i];
            }
            return null;
        }

        public override void Enter()
        {
            foreach (Simulation sim in simulationList)
                sim.Enter();
        }

        public override void Exit()
        {
            foreach (Simulation sim in simulationList)
                sim.Exit();
        }

        public override void Init()
        {
            Application.targetFrameRate = TargetFrameRate;
            // 必须用浮点数，两个整型做除法会被省略小数点
            perFrameCost = 1.0f / TargetFrameRate;
            curFrame = 1;
            cacheTime = 0;
            foreach (Simulation sim in simulationList)
                sim.Init();
        }

        public override void Tick()
        {
            foreach (Simulation sim in simulationList)
                sim.Tick();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            cacheTime += deltaTime;

            while (cacheTime > perFrameCost)
            {
                Tick();
                curFrame += 1;
                cacheTime -= perFrameCost;
            }
        }
    }
}
