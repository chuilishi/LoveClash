using Script.core;
using Script.Manager;
using Script.Minion;
using System.Collections;
using TMPro;
using UnityEngine;

public class MinionCard : MinionCardBase
{
    [SerializeField]
    private int minionTableId;
    private MinionTableItem minionTableItem;

    [SerializeField]
    private int minionSkillTableId;
    /// <summary>
    /// 随从技能卡
    /// </summary>
    private MinionSkillTableItem minionSkillTableItem;

    public MinionSkillTableItem MinionSkillTableItem
    {
        get
        {
            if (minionTableItem.skillCard == true)
            {
                if (minionSkillTableItem != null)
                {
                    return minionSkillTableItem;
                }
                else
                {
                    Debug.LogError("当前随从卡 skillCard 为 true 但随从技能卡为空");
                }
            }
            return null;
        }
        set { minionSkillTableItem = value; }
    }

    public override MinionTableItem MinionTableItem
    {
        get { return minionTableItem; }
        set { minionTableItem = value; }
    }

    public override int MinionTableId
    {
        get { return minionTableId; }
        set { minionTableId = value; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void onExtraExecute(PlayerBase playerBase)
    {
        base.onExtraExecute(playerBase);
    }
}
