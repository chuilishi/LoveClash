using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�����Ƶ�effectʵ��
[SerializeField]
public class BaseCardEffect : EffectBase
{
    public int heartRateChange = 0;
    public int trustChange = 0;
    public int excitementChange = 0;

    public override void Trigger(List<int> targetIds = null)
    {
        // ʵ��Ч���Ĵ���
        Myself.instance.��ͷֵ += heartRateChange;
        Myself.instance.����ֵ += trustChange;
        Myself.instance.�Ķ�ֵ += excitementChange;
    }
}
