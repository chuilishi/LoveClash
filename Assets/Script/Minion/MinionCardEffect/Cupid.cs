using Script.core;
using Script.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 无优先级，先不管
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class Cupid : MinionCardEffectBase
{
    private int value;

    /// <summary>
    /// 0：金箭，1：银箭
    /// </summary>
    private int positive;

    public override void OnDisabled()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnabled()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerEffect()
    {
        positive = (Random.Range(0, 1) == 0) ? 1 : -1;
        EventManager.Unregister<TurnOverEvent>(func);
    }

    private void func(TurnOverEvent @event)
    {
        Myself.instance.心动值 += value * positive;
        Opponent.instance.心动值 += value * positive;
        value++;
        //OperationExecutor.EndTurnEvent.RemoveListener(func);
    }
}