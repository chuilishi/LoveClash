using Script.Character;
using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 倒吊人的技能牌：无私奉献
/// </summary>
public class SelflessContribution : MinionSkillCardEffectBase
{
    public int value;

    public override void OnDisabled()
    {
        
    }

    public override void OnEnabled()
    {
        Myself.instance.心动值 -= value;
        _= new RandomCardCharacter().DrawCard();
    }
}
