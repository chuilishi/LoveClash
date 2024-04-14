using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 舔狗
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class Simp : MinionCardEffectBase
{
    //需要心动值增加的监听，暂时用简单布尔值
    bool heartRateAdd;
    int heartRateChange;
    int simpHeartRate = 15;

    public override void OnDisabled()
    {
        
    }

    public override void OnEnabled()
    {
        if (heartRateAdd)
        {
            simpHeartRate -= heartRateChange;
            if (simpHeartRate <= 0)
            {
                //销毁舔狗随从
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
