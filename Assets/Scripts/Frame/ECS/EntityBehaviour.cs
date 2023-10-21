using System;
using System.Collections.Generic;

namespace Frame
{
    /// <summary>
    /// 实体行为：管理System
    /// </summary>
    public class EntityBehaviour : IBehaviour
    {
        public List<IEntitySystem> systemList = new List<IEntitySystem>();

        public T AddSystem<T>() where T : IEntitySystem, new()
        {
            var system = new T();
            return AddSystem(system) as T;
        }

        public IEntitySystem AddSystem(IEntitySystem system)
        {
            if (!ExistSystem(system))
            {
                system.World = Simulation.GetWorld();
                system.Init();
                system.Enter();
                systemList.Add(system);
            }
            return system;
        }

        public bool ExistSystem(IEntitySystem system)
        {
            foreach (var item in systemList)
            {
                if (item == system)
                    return true;
                if (item.GetType() == system.GetType())
                    return true;
            }
            return false;
        }

        public override void Tick()
        {
            base.Tick();
            foreach (var item in systemList)
            {
                item.Tick();
            }
        }


    }
}
