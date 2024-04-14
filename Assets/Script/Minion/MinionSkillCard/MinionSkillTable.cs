using Script.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/MinionSkillTable", fileName = "MinionSkillTable")]
public class MinionSkillTable : ScriptableObject
{
    public List<MinionSkillTableItem> DataList = new List<MinionSkillTableItem>();

    public MinionSkillTableItem GetItemById(int id)
    {
        foreach (MinionSkillTableItem item in DataList)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        // 如果找不到对应 id 的元素，可以返回 null 或者抛出异常，视情况而定
        return null;
    }
}

/// <summary>
/// 具体某个随从技能
/// </summary>
[System.Serializable]
public class MinionSkillTableItem
{
    public int id;
    public string imagePath;
    public string prefabsPath;
    public string minionName;
    public string minionSkill;
    public bool skillCard = false;
    public int round = 0;
    public int remaindRound = 0;
    public MinionCardBase.FunctionType type = MinionCardBase.FunctionType.Opponent;
    public string decription;
    public MinionSkillCardBase minionSkillCard;
}
