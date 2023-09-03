using System;
using System.Collections.Generic;

namespace Frame
{
    /// <summary>
    /// 模拟器：管理行为与世界
    /// </summary>
    public class Simulation : ILogic
    {
        int Id;
        World World;
        List<IBehaviour> behaviourList;

        public Simulation(int id)
        {
            Id = id;
            World = World.Cteate();
            behaviourList = new();
        }

        public World GetWorld()
        {
            return World;
        }

        public int GetSimulationId()
        {
            return Id;
        }

        public T GetBehaviour<T>() where T : IBehaviour
        {
            foreach (var item in behaviourList)
            {
                if (item.GetType() == typeof(T))
                    return (T)item;
            }

            return default;
        }

        public T AddBehaviour<T>() where T : IBehaviour, new()
        {
            IBehaviour behaviour = new T();
            return AddBehaviour(behaviour) as T;
        }

        public IBehaviour AddBehaviour(IBehaviour behaviour)
        {
            if (!ExistBehaviour(behaviour))
            {
                behaviourList.Add(behaviour);
                behaviour.Simulation = this;
            }
            return behaviour;
        }

        public void RemoveBehaviour(IBehaviour behaviour)
        {
            if (ExistBehaviour(behaviour))
            {
                behaviourList.Remove(behaviour);
                behaviour.Simulation = null;
            }
        }

        public bool ExistBehaviour(IBehaviour behaviour)
        {
            foreach (var item in behaviourList)
            {
                if (item == behaviour) return true;
                if (item.GetType() == behaviour.GetType()) return true;
            }
            return false;
        }

        public void Init()
        {
            foreach (var item in behaviourList)
            {
                item.Init();
            }
        }

        public void Enter()
        {
            foreach (var item in behaviourList)
            {
                item.Enter();
            }
        }

        public void Tick()
        {
            foreach (var item in behaviourList)
            {
                item.Tick();
            }
        }

        public void Exit()
        {
            foreach (var item in behaviourList)
            {
                item.Exit();
            }
        }


    }
}
