using System;
using System.Collections.Generic;

namespace Frame
{
    /// <summary>
    /// 模拟器管理
    /// </summary>
    public class SimulationManager : Singleton<SimulationManager>, ILogic
    {
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

        public void Enter()
        {
            foreach (Simulation sim in simulationList)
                sim.Enter();
        }

        public void Exit()
        {
            foreach (Simulation sim in simulationList)
                sim.Exit();
        }

        public void Init()
        {
            foreach (Simulation sim in simulationList)
                sim.Init();
        }

        public void Tick()
        {
            foreach (Simulation sim in simulationList)
                sim.Tick();
        }
    }
}
