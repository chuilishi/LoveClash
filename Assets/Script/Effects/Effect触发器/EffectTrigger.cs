using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//基础牌效果触发
public class EffectTrigger : MonoBehaviour
{
    public void TriggerEffects(List<int> targetIds)
    {
        // 获取具有 BaseCardEffect 组件的所有游戏对象
        EffectBase[] effects = GetComponents<EffectBase>();

        // 遍历这些游戏对象并触发它们的 Effect
        foreach (EffectBase effect in effects)
        {
            effect.Trigger(targetIds);
        }
    }
}
