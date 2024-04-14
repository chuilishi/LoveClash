using Script.core;
using Script.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UIElements;
using Script.Manager;

/// <summary>
/// ��ħ
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class Succubus : MinionCardEffectBase
{
    //���ʣ����ûغ���
    private int disableRound = 1;

    private MinionCardEffectBase minionCardEffectBase;

    //���Ľ�����¼�غ���������غϲ�����ⲿ��ɻغϵļ���
    public override void OnEnabled()
    {
        minionCardEffectBase = UIManager.instance.opponentView.minions[new System.Random().Next(0, UIManager.instance.opponentView.minions.Count)].GetComponent<MinionCardEffectBase>();
        //�ݶ�Ϊ���öԷ�������
        minionCardEffectBase.disable = true;
        EventManager.Register<TurnOverEvent>(Recovery);
    }

    /// <summary>
    /// �ָ�������״̬
    /// </summary>
    protected void Recovery(TurnOverEvent turnOverEvent)
    {
        if (disableRound == 0)
        {
            minionCardEffectBase.disable = false;
            EventManager.Unregister<TurnOverEvent>(Recovery);
            disableRound = 1;
        }
        else
        {
            disableRound--;
        }
    }

    /// <summary>
    /// ��������ʲô������
    /// </summary>
    public override void OnDisabled()
    {
        
    }
}
