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
        // ����Ҳ�����Ӧ id ��Ԫ�أ����Է��� null �����׳��쳣�����������
        return null;
    }
}

/// <summary>
/// ����ĳ�����
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
