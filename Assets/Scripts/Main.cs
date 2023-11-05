using UnityEngine;
using Frame;

public class Main : MonoBehaviour
{
    public Transform EnvTransform;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        InitSimulation();
    }

    void InitSimulation()
    {
        // 添加模拟器
        var sim = new Simulation(Define.Client_Simulation);
        Entry.SimulationManager.AddSimulation(sim);

        // 添加模拟器行为
        sim.AddBehaviour<InputBehaviour>();
        var behaviour = sim.AddBehaviour<EntityBehaviour>();
        behaviour.AddSystem<MoveSystem>();

        // 添加全局组件
        sim.GetWorld().AddComponent<EnvironmentComp>();

        // 场景管理
        Entry.SceneManager.InitEnv(EnvTransform);

        // 添加玩家实体
        var player = sim.GetWorld().AddEntity(Define.Player_EntityId);
        player.AddComponent<MoveComp>();
        var transComp = player.AddComponent<TransformComp>();
        var collider = player.AddComponent<SphereColliderComp>();
        collider.InitByEngineCollider(Player.GetComponent<CapsuleCollider>(), transComp);
        Entry.SceneManager.AddEntity(player);

        var render = Player.gameObject.AddComponent<MoveRender>();
        render.SetEntityId(Define.Player_EntityId);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Define.TestValue = 1;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Define.TestValue = 0;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        Entry.SceneManager.OnDraw();
    }
#endif
}
