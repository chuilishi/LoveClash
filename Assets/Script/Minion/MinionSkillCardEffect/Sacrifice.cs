using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ħ�ļ��ܣ��׼�
/// </summary>
public class Sacrifice : MinionSkillCardEffectBase
{
    /// <summary>
    /// Ҫ�׼������ id����������̻�ȡ
    /// </summary>
    public int id;
    public override void OnDisabled()
    {
        
    }

    /// <summary>
    /// ȱ�������Ƶķ�������ʱд�ɽ�����ӿ��� Effect
    /// </summary>
    public override void OnEnabled()
    {
        MinionCard minionCard = Myself.instance.GetCard<MinionCard>(id)[0];// as MinionCard
        minionCard.GetComponent<MinionCardEffectBase>().disable = true;
        minionCard.GetComponent<CardView>().PlayCard(UIManager.instance.myselfView);
    }
}
