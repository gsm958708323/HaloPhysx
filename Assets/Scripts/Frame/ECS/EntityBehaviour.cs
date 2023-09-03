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

        public EntityBehaviour AddSystem(IEntitySystem system)
        {
            if (!ExistSystem(system))
            {
                systemList.Add(system);
                system.World = Simulation.GetWorld();
            }
            return this;
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
