using Script.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCard : CounterCardBase
{
    [SerializeField]
    private int counterTableId;
    private CounterTableItem counterTableItem;
    private CounterType type;

    public override CounterTableItem CounterTableItem
    {
        get => counterTableItem; set => counterTableItem = value;
    }

    public override int CounterTableId
    {
        get => counterTableId; set => counterTableId = value;
    }

    public override CounterType Type
    {
        get => type; set => type = value;
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
