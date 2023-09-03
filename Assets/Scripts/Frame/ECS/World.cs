using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

namespace Frame
{
    public class World
    {
        /// <summary>
        /// 存储所有enitty
        /// </summary>
        Dictionary<Guid, Entity> entityDict;
        /// <summary>
        /// 存储玩家身上的所有组件
        /// </summary>
        Dictionary<Guid, List<IComponent>> compEntityDict;
        /// <summary>
        /// 存储相同类型的组件
        /// </summary>
        Dictionary<Type, List<IComponent>> compTypeDict;

        private World()
        {
            entityDict = new();
            compEntityDict = new();
            compTypeDict = new();
        }

        public static World Cteate()
        {
            return new World();
        }

        public void Reset()
        {
            foreach (var item in entityDict)
            {
                var entity = item.Value;
                entity.World = null;
                Entity.ObjectPool.Recycle(entity);
            }
            entityDict.Clear();

            compEntityDict.Clear();
            compTypeDict.Clear();
        }

        public List<IComponent> GetComponents<T>() where T : IComponent
        {
            if (compTypeDict.ContainsKey(typeof(T)))
                return compTypeDict[typeof(T)];
            return null;
        }

        public void AddComponent(IComponent comp)
        {
            // 处理组件
            var type = comp.GetType();
            if (!compTypeDict.ContainsKey(type))
                compTypeDict[type] = new List<IComponent>();
            if (!compTypeDict[type].Contains(comp))
                compTypeDict[type].Add(comp);

            // 处理组件对应实体
            if (!compEntityDict.ContainsKey(comp.EntityId))
                compEntityDict[comp.EntityId] = new List<IComponent>();
            if (!compEntityDict[comp.EntityId].Contains(comp))
                compEntityDict[comp.EntityId].Add(comp);
        }

        public void RemoveComponent(IComponent comp)
        {
            Type type = comp.GetType();
            if (compTypeDict.ContainsKey(type))
                if (compTypeDict[type].Contains(comp))
                    compTypeDict[type].Remove(comp);

            if (compEntityDict.ContainsKey(comp.EntityId))
                if (compEntityDict[comp.EntityId].Contains(comp))
                    compEntityDict[comp.EntityId].Remove(comp);
        }

        public IComponent GetComponent(Guid entityId, Type type)
        {
            if (compEntityDict.ContainsKey(entityId))
            {
                var comps = compEntityDict[entityId];
                foreach (var item in comps)
                {
                    if (item.GetType() == type)
                        return item;
                }
            }
            return null;
        }

        public List<Entity> GetEntities()
        {
            return entityDict.Values.ToList();
        }

        public bool ExistEntity(Guid entityId)
        {
            return entityDict.ContainsKey(entityId);
        }

        public Entity GetEntity(Guid id)
        {
            if (ExistEntity(id))
                return entityDict[id];
            return null;
        }

        public Entity AddEntity(Guid entityId)
        {
            if (entityDict.ContainsKey(entityId))
                return entityDict[entityId];

            Entity entity = Entity.ObjectPool.Get() as Entity;
            entity.World = this;
            entityDict[entityId] = entity;
            return entity;
        }

        public bool RemoveEntity(Guid entityId)
        {
            Entity entity = GetEntity(entityId);
            if (entity == null)
                return false;

            RemoveEntityComponentAll(entityId);
            entity.World = null;
            Entity.ObjectPool.Recycle(entity);
            entityDict.Remove(entityId);
            return true;
        }

        private void RemoveEntityComponentAll(Guid entityId)
        {
            if (compEntityDict.ContainsKey(entityId))
            {
                var compList = compEntityDict[entityId];
                for (int i = compList.Count - 1; i >= 0; i--)
                {
                    var comp = compList[i];
                    if (comp != null)
                        RemoveComponent(comp);
                }

                compEntityDict.Remove(entityId);
            }
        }
    }
}
