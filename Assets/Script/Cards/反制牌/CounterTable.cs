using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/CounterTable", fileName = "CounterTable")]
public class CounterTable : ScriptableObject
{
    public List<CounterTableItem> DataList = new List<CounterTableItem>();

    public CounterTableItem GetItemById(int id)
    {
        foreach (CounterTableItem item in DataList)
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
/// ����ĳ��������
/// </summary>
[System.Serializable]
public class CounterTableItem
{
    public int id;
    public string imagePath;
    public string prefabsPath;
    public string counterName;
    public int round = 0;
    public int remaindRound = 0;
    public CounterCardBase.CounterType type = CounterCardBase.CounterType.MinionCard;
    public string decription;
    public CounterCardBase counterCard;
}
