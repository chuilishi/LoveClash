using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(MinionCardEffect))]
public abstract class MinionSkillCardEffectBase : NetworkObject
{
    private MinionSkillCardEffect minionSkillCardEffect;

    /// <summary>
    /// �Ƿ��ڽ���״̬
    /// </summary>
    public bool disable;

    protected virtual void Awake()
    {
        minionSkillCardEffect = GetComponent<MinionSkillCardEffect>();
        minionSkillCardEffect.onTrigger += OnTriggerEffect;
    }

    protected virtual void OnTriggerEffect()
    {
        if (!disable) { OnEnabled(); }
        else { OnDisabled(); }
    }

    /// <summary>
    /// ����ʱ���ã�����ĸ d �� Unity �����������ֿ���
    /// </summary>
    public abstract void OnEnabled();

    /// <summary>
    /// ������ʱ���ã�������ħ�ļ��ܿɽ������
    /// </summary>
    public abstract void OnDisabled();
}
