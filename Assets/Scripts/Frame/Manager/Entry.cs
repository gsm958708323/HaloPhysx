using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.TypeSearch;
using UnityEngine;

namespace Frame
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public partial class Entry : MonoSingleton<Entry>
    {
        /// <summary>
        /// 管理器组成的链表，优先级高的排在前面
        /// </summary>
        private LinkedList<IManager> managerLinked;
        public static SimulationManager SimulationManager;
        public static DriverManager DriverManager;
        public static TestManager TestManager;

        protected override void Awake()
        {
            base.Awake();
            managerLinked = new();

            InitManager();
        }

        private void InitManager()
        {
            SimulationManager = GetManager<SimulationManager>();
            DriverManager = GetManager<DriverManager>();
            TestManager = GetManager<TestManager>();
        }

        private void Update()
        {
            //更新：优先级高的先更新
            foreach (var item in managerLinked)
            {
                item.Update(Time.deltaTime);
            }
        }

        protected override void OnDestroy()
        {
            // 销毁：优先级低的先销毁
            for (var current = managerLinked.Last; current != null; current = current.Previous)
            {
                current.Value.Exit();
            }
            managerLinked.Clear();
            base.OnDestroy();
        }

        public T GetManager<T>() where T : IManager
        {
            var type = typeof(T);
            foreach (var item in managerLinked)
            {
                if (item.GetType() == type)
                    return item as T;
            }

            return AddManager(type) as T;
        }

        IManager AddManager(Type type)
        {
            IManager manager = (IManager)Activator.CreateInstance(type, true);
            if (manager == null)
            {
                Debugger.LogError($"[管理器] 创建管理器失败 {type.FullName}", LogDomain.Manager);
                return null;
            }

            LinkedListNode<IManager> current = managerLinked.First;
            while (current != null)
            {
                if (manager.Priority > current.Value.Priority)
                    break;

                current = current.Next;
            }

            if (current != null)
            {
                //找到一个比新节点优先级小的节点，把新节点放在它前面
                managerLinked.AddBefore(current, manager);
            }
            else
            {
                //否则，新新节点优先级最低，放在最后
                managerLinked.AddLast(manager);
            }

            manager.Init();
            manager.Enter();
            return manager;
        }
    }
}
