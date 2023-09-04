using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    public class Render : MonoBehaviour
    {
        protected Simulation Simulation;
        protected Guid EntityId;

        private void Awake()
        {
            Simulation = Entry.SimulationManager.GetSimulation(Define.Client_Simulation);
        }

        public void SetEntityId(Guid entityId)
        {
            EntityId = entityId;
        }

        // Update is called once per frame
        void Update()
        {
            var entity = Simulation.GetWorld().GetEntity(EntityId);
            if (entity == null)
                return;
            if (entity.LifeState == 0)
                return;
            OnUpdate(entity);
        }

        protected virtual void OnUpdate(Entity entity)
        {
        }
    }
}
