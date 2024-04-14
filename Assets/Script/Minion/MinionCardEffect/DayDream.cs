using Script.Character;
using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 白日梦
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class DayDream : MinionCardEffectBase
{
    public override void OnDisabled()
    {
        
    }

    public override void OnEnabled()
    {
        //暂时为抽随从卡
        //_ = Myself.instance.DrawCard();
        _ = new RandomMinionCharacter().DrawCard();
        Destroy(gameObject);
    }
}
