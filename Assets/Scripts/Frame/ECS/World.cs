using System;
using System.Collections.Generic;
using System.Linq;

namespace Frame
{
    /// <summary>
    /// 世界：管理组件和实体
    /// </summary>
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

            // AddComponentCustom(comp);
        }

        /// <summary>
        /// 组件自定义逻辑
        /// </summary>
        /// <param name="comp"></param>
        void AddComponentCustom(IComponent comp)
        {
            if (comp is TransformComp)
            {
                var entity = GetEntity(comp.EntityId);
                if (entity != null)
                {
                    // tips: 这时TransformComp的数据还没有完全初始化完成，需要手动添加
                    Entry.SceneManager.AddEntity(entity);
                }
            }
        }

        public void RemoveComponent(IComponent comp)
        {
            RemoveComponentCustom(comp);
            Type type = comp.GetType();
            if (compTypeDict.ContainsKey(type))
                if (compTypeDict[type].Contains(comp))
                    compTypeDict[type].Remove(comp);

            if (compEntityDict.ContainsKey(comp.EntityId))
                if (compEntityDict[comp.EntityId].Contains(comp))
                    compEntityDict[comp.EntityId].Remove(comp);
        }

        public void RemoveComponentCustom(IComponent comp)
        {
            if (comp is TransformComp)
            {
                var entity = GetEntity(comp.EntityId);
                if (entity != null)
                {
                    Entry.SceneManager.RemoveEntity(entity);
                }
            }
        }

        /// <summary>
        /// 获取某个实体身上的组件
        /// </summary>
        public IComponent GetComponentByEntityId(Guid entityId, Type type)
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

        /// <summary>
        /// 获取同类型下的所有组件，包含全局组件
        /// </summary>
        /// <returns></returns>
        public List<T> GetComponents<T>() where T : IComponent
        {
            if (compTypeDict.ContainsKey(typeof(T)))
            {
                var list = compTypeDict[typeof(T)];
                var result = new List<T>();
                foreach (var item in list)
                {
                    result.Add(item as T);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 添加全局组件，不跟随实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>() where T : IComponent, new()
        {
            IComponent comp = new T();
            AddComponent(comp);
            return comp as T;
        }

        /// <summary>
        /// 获取全局组件
        /// </summary>
        /// <returns></returns>
        public IComponent GetComponent<T>() where T : IComponent
        {
            if (compTypeDict.ContainsKey(typeof(T)))
                return compTypeDict[typeof(T)][0];
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
            entity.Id = entityId;
            entityDict[entityId] = entity;
            return entity;
        }

        public bool RemoveEntity(Guid entityId)
        {
            Entity entity = GetEntity(entityId);
            if (entity == null)
                return false;

            Entry.SceneManager.RemoveEntity(entity);
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
