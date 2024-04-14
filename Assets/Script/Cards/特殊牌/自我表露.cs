using Script.Cards;
using Script.core;
using Script.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 打出后，如果三回合后，心动值达到10，则信任值＋3，如果没达到，信任值-5
/// </summary>

public class 自我表露: SpecialCardBase
{
    int roundCount = 3;
    public override void Execute(PlayerBase player,List<int> targetIds = null)
    {
        EventManager.Register<TurnOverEvent>(func);
    }
    private void func(TurnOverEvent @event)
    {
        if (@event.playerEnum == NetworkManager.instance.playerEnum)
        {
            roundCount--;
            if (roundCount == 0)
            {
                if (Myself.instance.心动值 >= 10)
                {
                    Myself.instance.信任值 += 3;
                }
                else
                {
                    Myself.instance.信任值 -= 5;
                }
                EventManager.Unregister<TurnOverEvent>(func);
            }
        }
    }
}