using System;
using System.Collections.Generic;
using UnityEngine;

namespace Frame
{
    /// <summary>
    /// 驱动管理：给代码赋予update能力
    /// </summary>
    public class DriverManager : IManager
    {
        List<IDriver> driverList;
        List<IDriver> waitList;

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (waitList.Count > 0)
            {
                foreach (var item in waitList)
                {
                    item.Init();
                    item.Enter();

                    driverList.Add(item);
                }
                waitList.Clear();
            }

            foreach (var item in driverList)
            {
                item.Update(deltaTime);
            }
        }

        public override void Enter()
        {
            base.Enter();
            driverList = new();
            waitList = new();
        }

        public override void Exit()
        {
            waitList.Clear();

            foreach (var item in driverList)
            {
                item.Exit();
            }
            driverList.Clear();

            base.Exit();
        }

        public void AddDriver(IDriver driver)
        {
            if (driver == null)
            {
                Debugger.LogError($"添加驱动失败 {driver.GetType().FullName}", LogDomain.Driver);
                return;
            }

            waitList.Add(driver);
        }

        public T AddDriver<T>() where T : IDriver, new()
        {
            var driver = new T();
            AddDriver(driver);
            return driver;
        }

        public T RemoveDriver<T>() where T : IDriver, new()
        {
            var driver = new T();
            RemoveDriver(driver);
            return null;
        }

        public void RemoveDriver(IDriver driver)
        {
            if (driver == null)
            {
                Debugger.LogError($"移除驱动失败 {driver.GetType().FullName}", LogDomain.Driver);
                return;
            }

            if (waitList.Contains(driver))
            {
                waitList.Remove(driver);
            }
            driver.Exit();
        }
    }
}
