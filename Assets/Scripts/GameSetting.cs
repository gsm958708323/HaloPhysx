using System;
using UnityEngine;

public class GameSetting : MonoSingleton<GameSetting>
{
    public int TargetFrameRate = 60;
    public Action<int> OnTick;

    float perFrameCost;
    float cacheTime;
    int curFrame;

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = TargetFrameRate;
        perFrameCost = 1 / TargetFrameRate;
        curFrame = 1;
        cacheTime = 0;
    }

    void Update()
    {
        cacheTime += Time.deltaTime;

        while (cacheTime > perFrameCost)
        {
            if (OnTick != null)
            {
                OnTick(curFrame);
                curFrame += 1;
                cacheTime -= perFrameCost;
            }
        }
    }
}
