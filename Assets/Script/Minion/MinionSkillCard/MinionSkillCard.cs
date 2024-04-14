using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSkillCard : MinionSkillCardBase
{
    [SerializeField]
    private int minionSkillTableId;
    /// <summary>
    /// ��Ӽ��ܿ�
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

        //��ʱдΪ���� 1 ����Ů�� 0.5
        playerBase.��ͷֵ���� = playerBase.��ͷֵ���� / ((int)playerBase.�Ա� + 1);
    }
}
