using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//基础牌的effect实现
[SerializeField]
public class BaseCardEffect : EffectBase
{
    public int heartRateChange = 0;
    public int trustChange = 0;
    public int excitementChange = 0;

    public override void Trigger(List<int> targetIds = null)
    {
        // 实现效果的代码
        Myself.instance.上头值 += heartRateChange;
        Myself.instance.信任值 += trustChange;
        Myself.instance.心动值 += excitementChange;
    }
}
