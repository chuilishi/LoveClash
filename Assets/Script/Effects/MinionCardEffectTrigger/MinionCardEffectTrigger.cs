using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCardEffectTrigger : MonoBehaviour
{
    public void TriggerEffects(List<int> targetIds)
    {
        // 获取具有 BaseCardEffect 组件的所有游戏对象
        MinionCardEffect[] effects = GetComponents<MinionCardEffect>();

        // 遍历这些游戏对象并触发它们的 Effect
        foreach (MinionCardEffect effect in effects)
        {
            effect.Trigger(targetIds);
        }
    }
}
