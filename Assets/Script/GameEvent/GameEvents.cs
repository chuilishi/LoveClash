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

// 游戏开始
public class GameStartEvent : GameEvent<GameStartEvent>
{
    
}

// 游戏结束
public class GameOverEvent : GameEvent<GameOverEvent>
{
    public PlayerEnum winner;
}

// 回合开始
public class TurnStartEvent : GameEvent<TurnStartEvent>
{
    public PlayerEnum playerEnum;
}

// 回合中
public class TurnRunningEvent : GameEvent<TurnRunningEvent>
{
    public PlayerEnum playerEnum;
}

// 回合结束
public class TurnOverEvent : GameEvent<TurnOverEvent>
{
    public PlayerEnum playerEnum;
}

// 使用卡牌
public class PlayCardEvent : GameEvent<PlayCardEvent>
{
    public int cardId;
    public PlayerEnum playerEnum;
    public CounterCardBase.CounterType counterType;
}

// 使用基础牌
public class PlayBaseCardEvent : GameEvent<PlayBaseCardEvent>
{
    public int cardId;
    public PlayerEnum playerEnum;
}

// 使用随从技能牌
public class PlayMinionSkillCardEvent : GameEvent<PlayMinionSkillCardEvent>
{
    public int cardId;
    public PlayerEnum playerEnum;
}

// 卡牌效果
public class CardEffectEvent : GameEvent<CardEffectEvent>
{
    //public bool disable;
}

// 反制
public class CounterEvent : GameEvent<CounterEvent>
{
    public bool disable;
}

// 用技能
public class PlaySkillEvent : GameEvent<PlaySkillEvent>
{
    public PlayerEnum playerEnum;
}

// 抽卡
public class DrawCardEvent : GameEvent<DrawCardEvent>
{
    public PlayerEnum playerEnum;
}