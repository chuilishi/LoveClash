using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 恶魔的技能：献祭
/// </summary>
public class Sacrifice : MinionSkillCardEffectBase
{
    /// <summary>
    /// 要献祭的随从 id，需额外流程获取
    /// </summary>
    public int id;
    public override void OnDisabled()
    {
        
    }

    /// <summary>
    /// 缺少销毁牌的方法，暂时写成禁用随从卡的 Effect
    /// </summary>
    public override void OnEnabled()
    {
        MinionCard minionCard = Myself.instance.GetCard<MinionCard>(id)[0];// as MinionCard
        minionCard.GetComponent<MinionCardEffectBase>().disable = true;
        minionCard.GetComponent<CardView>().PlayCard(UIManager.instance.myselfView);
    }
}
