using Script.Cards;
using Script.core;
using Script.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 皇帝，改进——需要抽牌监听
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class Emperor : MinionCardEffectBase
{
    PlayerBase playerBase;
    CardBase cardBase;

    public override void OnDisabled()
    {
        if (init) init = false;
        else return;
        EventManager.Unregister<DrawCardEvent>(Listener);
    }

    public override void OnEnabled()
    {
        if (!init) init = true;
        else return;
        //需要抽牌监听
        EventManager.Register<DrawCardEvent>(Listener);
    }

    private async void Listener(DrawCardEvent @event)
    {
        cardBase = (CardBase)await playerBase.DrawCard();
        EventManager.Register<TurnOverEvent>(func);
    }

    private void func(TurnOverEvent @event)
    {
        if (UnityEngine.Random.Range(0, 1) == 1)
        {
            if (cardBase != null)
                Destroy(cardBase.gameObject);
            EventManager.Unregister<TurnOverEvent>(func);
            
            //重置
            cardBase = null;
        }
    }
}
