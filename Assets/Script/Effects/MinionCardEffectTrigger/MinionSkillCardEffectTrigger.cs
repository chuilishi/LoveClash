using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSkillCardEffectTrigger : MonoBehaviour
{
    public void TriggerEffects()
    {
        // ��ȡ���� BaseCardEffect �����������Ϸ����
        MinionSkillCardEffect[] effects = GetComponents<MinionSkillCardEffect>();

        // ������Щ��Ϸ���󲢴������ǵ� Effect
        foreach (MinionSkillCardEffect effect in effects)
        {
            effect.Trigger();
        }
    }
}
