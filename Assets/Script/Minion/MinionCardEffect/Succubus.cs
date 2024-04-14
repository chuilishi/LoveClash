using Script.core;
using Script.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UIElements;
using Script.Manager;

/// <summary>
/// 魅魔
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class Succubus : MinionCardEffectBase
{
    //随从剩余禁用回合数
    private int disableRound = 1;

    private MinionCardEffectBase minionCardEffectBase;

    //待改进：记录回合数、计算回合差，建议外部完成回合的计算
    public override void OnEnabled()
    {
        minionCardEffectBase = UIManager.instance.opponentView.minions[new System.Random().Next(0, UIManager.instance.opponentView.minions.Count)].GetComponent<MinionCardEffectBase>();
        //暂定为禁用对方随机随从
        minionCardEffectBase.disable = true;
        EventManager.Register<TurnOverEvent>(Recovery);
    }

    /// <summary>
    /// 恢复到启用状态
    /// </summary>
    protected void Recovery(TurnOverEvent turnOverEvent)
    {
        if (disableRound == 0)
        {
            minionCardEffectBase.disable = false;
            EventManager.Unregister<TurnOverEvent>(Recovery);
            disableRound = 1;
        }
        else
        {
            disableRound--;
        }
    }

    /// <summary>
    /// 被禁用则什么都不做
    /// </summary>
    public override void OnDisabled()
    {
        
    }
}
