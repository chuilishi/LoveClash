using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������Ч������
public class EffectTrigger : MonoBehaviour
{
    public void TriggerEffects(List<int> targetIds)
    {
        // ��ȡ���� BaseCardEffect �����������Ϸ����
        EffectBase[] effects = GetComponents<EffectBase>();

        // ������Щ��Ϸ���󲢴������ǵ� Effect
        foreach (EffectBase effect in effects)
        {
            effect.Trigger(targetIds);
        }
    }
}
