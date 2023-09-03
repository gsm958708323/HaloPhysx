using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDriver
{
    void Init();
    void Enter();
    void Tick();
    void Exit();
}

namespace Frame
{
    /// <summary>
    /// 框架驱动
    /// </summary>
    public class Driver : MonoBehaviour
    {
        LinkedList<IDriver> drivers = new();
        Queue<IDriver> driversQueue = new();

        public void AddDriver(IDriver driver)
        {

        }
    }
}
