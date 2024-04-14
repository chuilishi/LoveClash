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

        // ����Ҳ�����Ӧ id ��Ԫ�أ����Է��� null �����׳��쳣�����������
        return null;
    }
}

/// <summary>
/// ����ĳ����Ӽ���
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
