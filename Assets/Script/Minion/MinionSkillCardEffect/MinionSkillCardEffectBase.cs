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
    /// 是否处于禁用状态
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
    /// 启用时调用（加字母 d 和 Unity 生命周期区分开）
    /// </summary>
    public abstract void OnEnabled();

    /// <summary>
    /// 被禁用时调用，例如魅魔的技能可禁用随从
    /// </summary>
    public abstract void OnDisabled();
}
