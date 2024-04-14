using Script.core;
using Script.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class TestEvent : GameEvent<TestEvent>
{
    public string message;
}

// ��Ϸ��ʼ
public class GameStartEvent : GameEvent<GameStartEvent>
{
    
}

// ��Ϸ����
public class GameOverEvent : GameEvent<GameOverEvent>
{
    public PlayerEnum winner;
}

// �غϿ�ʼ
public class TurnStartEvent : GameEvent<TurnStartEvent>
{
    public PlayerEnum playerEnum;
}

// �غ���
public class TurnRunningEvent : GameEvent<TurnRunningEvent>
{
    public PlayerEnum playerEnum;
}

// �غϽ���
public class TurnOverEvent : GameEvent<TurnOverEvent>
{
    public PlayerEnum playerEnum;
}

// ʹ�ÿ���
public class PlayCardEvent : GameEvent<PlayCardEvent>
{
    public int cardId;
    public PlayerEnum playerEnum;
    public CounterCardBase.CounterType counterType;
}

// ʹ�û�����
public class PlayBaseCardEvent : GameEvent<PlayBaseCardEvent>
{
    public int cardId;
    public PlayerEnum playerEnum;
}

// ʹ����Ӽ�����
public class PlayMinionSkillCardEvent : GameEvent<PlayMinionSkillCardEvent>
{
    public int cardId;
    public PlayerEnum playerEnum;
}

// ����Ч��
public class CardEffectEvent : GameEvent<CardEffectEvent>
{
    //public bool disable;
}

// ����
public class CounterEvent : GameEvent<CounterEvent>
{
    public bool disable;
}

// �ü���
public class PlaySkillEvent : GameEvent<PlaySkillEvent>
{
    public PlayerEnum playerEnum;
}

// �鿨
public class DrawCardEvent : GameEvent<DrawCardEvent>
{
    public PlayerEnum playerEnum;
}