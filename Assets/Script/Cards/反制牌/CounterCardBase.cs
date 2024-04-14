using Script.Cards;
using Script.core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CounterCardBase;

/// <summary>
/// ������
/// </summary>
public abstract class CounterCardBase : CardBase, ICounter
{
    public abstract int CounterTableId { get; set; }

    public abstract CounterTableItem CounterTableItem { get; set; }
    public abstract CounterType Type { get; set; }

    public enum CounterType
    {
        AnyCard,
        BaseCard,
        MinionCard,
        MinionSkillCard,
        /// <summary>
        /// �����Ʊ���Чʱ��� Null
        /// </summary>
        Null
    }

    public override void Execute(PlayerBase playerBase, List<int> targetIds = null)
    {
        onExtraExecute(playerBase);
    }
    public virtual void onExtraExecute(PlayerBase playerBase)
    {

    }

    protected CounterTableItem GetCounterTableItemById(int id)
    {
        return TableManager.instance.GetCounterTable().GetItemById(id);
    }

    protected void GetCounterTableItemById()
    {
        var counter = TableManager.instance.GetCounterTable();
        CounterTableItem = counter.GetItemById(CounterTableId);
    }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        GetCounterTableItemById();
        transform.Find("NameIcon").GetComponentInChildren<TextMeshProUGUI>().text = CounterTableItem.counterName;
        transform.Find("Description").GetComponent<TextMeshProUGUI>().text = CounterTableItem.decription;
        Type = CounterTableItem.type;
    }

    public virtual void Counter(PlayCardEvent playCardEvent)
    {

    }
}

public interface ICounter
{
    /// <summary>
    /// ���ƵĿ�������
    /// </summary>
    CounterType Type { get; set; }

    /// <summary>
    /// ���Ʒ���
    /// </summary>
    /// <param name="drawCardEvent"></param>
    void Counter(PlayCardEvent playCardEvent);

    // ��������
    //string MyProperty { get; set; }


    // ����������
    //int this[int index] { get; set; }
}