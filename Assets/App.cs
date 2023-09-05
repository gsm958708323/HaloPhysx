using System.Collections;
using System.Collections.Generic;
using Frame;
using UnityEngine;

public class App : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sim = new Simulation(Define.Client_Simulation);
        sim.AddBehaviour<InputBehaviour>();
        
        var behaviour = sim.AddBehaviour<EntityBehaviour>();
        behaviour.AddSystem<MoveSystem>();
        Entry.SimulationManager.AddSimulation(sim);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
