using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSkillCard : MinionSkillCardBase
{
    [SerializeField]
    private int minionSkillTableId;
    /// <summary>
    /// 随从技能卡
    /// </summary>
    private MinionSkillTableItem minionSkillTableItem;

    public override MinionSkillTableItem MinionSkillTableItem
    {
        get { return minionSkillTableItem; }
        set { minionSkillTableItem = value; }
    }

    public override int MinionSkillTableId
    {
        get { return minionSkillTableId; }
        set { minionSkillTableId = value; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void onExtraExecute(PlayerBase playerBase)
    {
        base.onExtraExecute(playerBase);

        //暂时写为男性 1 倍，女性 0.5
        playerBase.上头值倍率 = playerBase.上头值倍率 / ((int)playerBase.性别 + 1);
    }
}
