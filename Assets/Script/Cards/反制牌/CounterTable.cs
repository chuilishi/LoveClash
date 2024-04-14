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

        // 如果找不到对应 id 的元素，可以返回 null 或者抛出异常，视情况而定
        return null;
    }
}

/// <summary>
/// 具体某个反制牌
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
