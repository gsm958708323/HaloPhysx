using UnityEngine;
using Frame;

public class MoveDriver : IDriver
{
    GameObject player;
    public override void Init()
    {
        player = GameObject.Find("Player");
    }

    public override void Update(float deltaTime)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        player.transform.Translate(new Vector3(h, 0, v) * deltaTime * 5);
    }
}

