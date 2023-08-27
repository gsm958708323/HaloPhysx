using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        // 获取输入方向
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 计算移动方向
        Vector3 dir = new Vector3(h, 0, v);
        var qua = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position += qua * dir * speed * Time.deltaTime;
    }
}
