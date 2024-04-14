using Script.core;
using Script.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���¹�
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class WhiteMoonlight : MinionCardEffectBase
{
    public int value = 1;

    private void func(TurnOverEvent turnOverEvent)
    {
        Myself.instance.�Ķ�ֵ -= value;
        Myself.instance.��ͷֵ -= value;
    }

    public override void OnEnabled()
    {
        EventManager.Register<TurnOverEvent>(func);
    }

    public override void OnDisabled()
    {
        EventManager.Unregister<TurnOverEvent>(func);
    }
}

