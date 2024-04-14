using Script.core;
using Script.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : NetworkObject
{
    public static TableManager instance;

    private void Awake()
    {
        instance = this;
        GetMinionTable();
        GetMinionSkillTable();
    }

    private MinionTable minionTable;

    public MinionTable GetMinionTable()
    {
        if (minionTable == null)
        {
            minionTable = Resources.Load<MinionTable>("TableData/MinionTable");
        }
        return minionTable;
    }

    private MinionSkillTable minionSkillTable;

    public MinionSkillTable GetMinionSkillTable()
    {
        if (minionSkillTable == null)
        {
            minionSkillTable = Resources.Load<MinionSkillTable>("TableData/MinionSkillTable");
        }
        return minionSkillTable;
    }

    private CounterTable counterTable;

    public CounterTable GetCounterTable()
    {
        if (counterTable == null)
        {
            counterTable = Resources.Load<CounterTable>("TableData/CounterTable");
        }
        return counterTable;
    }
}
