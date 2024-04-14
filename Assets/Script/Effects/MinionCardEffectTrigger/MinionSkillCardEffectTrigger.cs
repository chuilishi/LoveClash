using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSkillCardEffectTrigger : MonoBehaviour
{
    public void TriggerEffects()
    {
        // 获取具有 BaseCardEffect 组件的所有游戏对象
        MinionSkillCardEffect[] effects = GetComponents<MinionSkillCardEffect>();

        // 遍历这些游戏对象并触发它们的 Effect
        foreach (MinionSkillCardEffect effect in effects)
        {
            effect.Trigger();
        }
    }
}
