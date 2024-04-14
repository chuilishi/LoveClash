using Script.Character;
using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第三者的技能牌：绿帽子
/// </summary>
public class GreenHat : MinionSkillCardEffectBase
{
    public int value;

    public override void OnDisabled()
    {

    }

    public override void OnEnabled()
    {
        Myself.instance.信任值 -= value;
    }
}
