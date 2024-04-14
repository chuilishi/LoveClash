using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinionCardEffect))]
public abstract class MinionCardEffectBase : NetworkObject
{
    private MinionCardEffect minionCardEffect;

    protected int round;
    protected int remaindRound;

    /// <summary>
    /// 防止重复生效的参数，涉及到比如倍率、监听时可以用
    /// </summary>
    protected bool init;

    /// <summary>
    /// 本回合是否已经用过——似乎不太需要
    /// </summary>
    protected bool used;

    /// <summary>
    /// 是否处于禁用状态
    /// </summary>
    public bool disable;

    protected virtual void Awake()
    {
        minionCardEffect = GetComponent<MinionCardEffect>();
        round = GetComponent<MinionCard>().MinionTableItem.round;
        remaindRound = GetComponent<MinionCard>().MinionTableItem.remaindRound;
        if (GetComponent<MinionCard>().MinionTableItem.round != 0)
        {
            minionCardEffect.onTrigger += RoundJudge;
            minionCardEffect.onTrigger += OnTriggerEffect;
        }
        else
        {
            //回合数为 0（也就是无冷却时间卡牌）则直接启用，这里不使用事件注册可以防止反复触发——待改进，比如触发一次即注销
            OnTriggerEffect();
        }
        EventManager.Register<TurnOverEvent>(RoundCount);
    }

    /// <summary>
    /// 回合数计算
    /// </summary>
    protected void RoundCount(TurnOverEvent turnOverEvent)
    {
        remaindRound = Mathf.Max(0, --remaindRound);
        used = false;
    }

    /// <summary>
    /// 获取随从技能卡
    /// </summary>
    protected void DrawMinionSkillCard()
    {
        int id = GetComponent<MinionCard>().MinionTableItem.minionSkillCardId;
        if (id != 0)
        {
            UIManager.instance.myselfView.DrawCard<MinionSkillCard>(GetComponent<ICardView>(), id);
        }
    }

    /// <summary>
    /// 回合数判定，剩余回合数为 0 则 disable 为 false
    /// </summary>
    protected void RoundJudge()
    {
        if (remaindRound <= 0)
        {
            remaindRound = round;
            disable = false;
        }
        else
        {
            disable = true;
        }
    }

    protected virtual void OnTriggerEffect()
    {
        if (!used) used = true;
        else return;
        if (!disable) { OnEnabled(); DrawMinionSkillCard(); }
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

    private void OnDisable()
    {
        minionCardEffect.onTrigger -= OnTriggerEffect;
        minionCardEffect.onTrigger -= RoundJudge;
        EventManager.Unregister<TurnOverEvent>(RoundCount);
    }
}
