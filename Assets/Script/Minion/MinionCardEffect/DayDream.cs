using Script.Character;
using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class DayDream : MinionCardEffectBase
{
    public override void OnDisabled()
    {
        
    }

    public override void OnEnabled()
    {
        //��ʱΪ����ӿ�
        //_ = Myself.instance.DrawCard();
        _ = new RandomMinionCharacter().DrawCard();
        Destroy(gameObject);
    }
}
