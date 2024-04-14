using Script.Minion;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/MinionTable", fileName = "MinionTable")]
public class MinionTable : ScriptableObject
{   
    [SerializeField]
    public List<MinionTableItem> DataList = new List<MinionTableItem>();

    public MinionTableItem GetItemById(int id)
    {
        foreach (MinionTableItem item in DataList)
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
/// 具体某个随从
/// </summary>
[System.Serializable]
public class MinionTableItem
{
    public int id;
    public string imagePath;
    public string prefabsPath;
    public string minionName;
    public string minionSkill;
    public bool skillCard;
    public int minionSkillCardId = 0;
    public int round = 0;
    public int remaindRound = 0;
    public MinionCardBase.FunctionType type = MinionCardBase.FunctionType.Opponent;
    public string decription;
}
