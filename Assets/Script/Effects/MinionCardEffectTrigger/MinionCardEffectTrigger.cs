using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCardEffectTrigger : MonoBehaviour
{
    public void TriggerEffects(List<int> targetIds)
    {
        // ��ȡ���� BaseCardEffect �����������Ϸ����
        MinionCardEffect[] effects = GetComponents<MinionCardEffect>();

        // ������Щ��Ϸ���󲢴������ǵ� Effect
        foreach (MinionCardEffect effect in effects)
        {
            effect.Trigger(targetIds);
        }
    }
}
