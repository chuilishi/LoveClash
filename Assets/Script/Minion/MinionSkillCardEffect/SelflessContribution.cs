using Script.Character;
using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����˵ļ����ƣ���˽����
/// </summary>
public class SelflessContribution : MinionSkillCardEffectBase
{
    public int value;

    public override void OnDisabled()
    {
        
    }

    public override void OnEnabled()
    {
        Myself.instance.�Ķ�ֵ -= value;
        _= new RandomCardCharacter().DrawCard();
    }
}
