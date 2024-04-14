using Script.Cards;
using Script.core;
using Script.Manager;
using System.Collections.Generic;

public abstract class MinionSkillCardBase : CardBase
{
    public abstract int MinionSkillTableId { get; set; }
    public abstract MinionSkillTableItem MinionSkillTableItem { get; set; }

    public override void Execute(PlayerBase playerBase, List<int> targetIds = null)
    {
        onExtraExecute(playerBase);
    }

    public virtual void onExtraExecute(PlayerBase playerBase)
    {

    }

    protected MinionSkillTableItem GetMinionSkillTableItemById(int id)
    {
        return TableManager.instance.GetMinionSkillTable().GetItemById(id);
    }

    protected void GetMinionSkillTableItemById()
    {
        MinionSkillTableItem = TableManager.instance.GetMinionSkillTable().GetItemById(MinionSkillTableId);
    }

    protected virtual void Awake()
    {
        GetMinionSkillTableItemById();
    }
}
