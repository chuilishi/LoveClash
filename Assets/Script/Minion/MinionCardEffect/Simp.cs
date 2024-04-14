using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class Simp : MinionCardEffectBase
{
    //��Ҫ�Ķ�ֵ���ӵļ�������ʱ�ü򵥲���ֵ
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
                //���������
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
