using Script.Character;
using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ߵļ����ƣ���ñ��
/// </summary>
public class GreenHat : MinionSkillCardEffectBase
{
    public int value;

    public override void OnDisabled()
    {

    }

    public override void OnEnabled()
    {
        Myself.instance.����ֵ -= value;
    }
}
