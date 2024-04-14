using Script;
using Script.core;
using Script.Effects.其他Effect;
using Script.Manager;
using Script.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public enum GameState
{
    GameStart, // 游戏开始
    StartTurn, // 回合开始
    RunningTurn, // 回合中
    OverTurn, // 回合结束
    GameOver // 游戏结束
}

public static class GameProcessor
{
    private static GameState _state;
    public static GameState State => _state;

    private static PlayerEnum _curPlayer;
    public static PlayerEnum CurPlayer => _curPlayer;

    private static PlayerEnum _winner;

    private static void Init()
    {
        
    }

    public static void Start()
    {
        // 初始化游戏数据
        Init();

        // 初始化完成就通知游戏开始，有些效果可能游戏开始要触发
        _state = GameState.GameStart;
        using var evt = GameStartEvent.Get();
        EventManager.SendEvent(evt);
        Debug.Log($"游戏开始");

        // 设置一下先是谁的回合
        _curPlayer = GetStartPlayer();
        Run(GameState.StartTurn);
    }

    public static void Over()
    {
        _state = GameState.GameOver;
        var evt = GameOverEvent.Get();
        evt.winner = _winner;
        EventManager.SendEvent(evt);
        Debug.Log($"游戏结束");
    }

    public static void Run(GameState gameState)
    {
        _state = gameState;

        switch (gameState)
        {
            case GameState.StartTurn:
                OnStartTurn();
                break;
            case GameState.RunningTurn:
                OnRunningTurn();
                break;
            case GameState.OverTurn:
                OnOverTurn();
                break;
            default: break;
        }
    }

    public static void TurnOver(PlayerEnum playerEnum)
    {
        if (playerEnum != _curPlayer)
            return;

        Run(GameState.OverTurn);
    }

    private static void OnStartTurn()
    {
        using var evt = TurnStartEvent.Get();
        evt.playerEnum = CurPlayer;
        EventManager.SendEvent(evt);
        Debug.Log($"回合开始：{_curPlayer}");

        if (CurPlayer == NetworkManager.instance.playerEnum)
        {
            GameManager.instance.ExecuteOperation(new Operation(OperationType.Effect, extraMessage: typeof(DrawCardEffect).FullName));
        }

        if (CheckPlayerCanAction(_curPlayer))
        {
            //这里添加对对方反制牌的查询
            List<ICounter> counters = Opponent.instance.GetCounterCard();

            foreach (var counter in counters)
            {
                //方案 1
                //反制的优先级设为 高
                //EventManager.Register<PlayCardEvent>(counter.Counter, GameEventPriority.High);

                //方案 2
                /*switch (counter.Type)
                {
                    case CounterCardBase.CounterType.AnyCard:
                        EventManager.Register<PlayCardEvent>((playCardEvent) =>
                        {
                            counter.Counter();
                        }, GameEventPriority.High);
                        break;
                    case CounterCardBase.CounterType.BaseCard:
                        EventManager.Register<PlayBaseCardEvent>((playBaseCardEvent) => 
                        {
                            counter.Counter();
                        }, GameEventPriority.High);
                        break;
                    case CounterCardBase.CounterType.MinionSkillCard:
                        EventManager.Register<PlayMinionSkillCardEvent>((playMinionSkillCardEvent) =>
                        {
                            counter.Counter();
                        }, GameEventPriority.High);
                        break;
                }*/
            }

            Run(GameState.RunningTurn);
        }
        else
        {
            Run(GameState.OverTurn);
        }
    }

    private static void OnRunningTurn()
    {
        using var evt = TurnRunningEvent.Get();
        evt.playerEnum = CurPlayer;
        EventManager.SendEvent(evt);
        Debug.Log($"回合中：{_curPlayer}");
    }

    private static void OnOverTurn()
    {
        using var evt = TurnOverEvent.Get();
        evt.playerEnum = CurPlayer;
        EventManager.SendEvent(evt);
        Debug.Log($"回合结束：{_curPlayer}");

        if (!CheckGameOver(out _winner))
        {
            _curPlayer = CurPlayer == PlayerEnum.Player1 ? PlayerEnum.Player2 : PlayerEnum.Player1;
            Run(GameState.StartTurn);
        }
        else
        {
            Over();
        }
    }

    /// <summary>
    /// 返回开始的玩家
    /// </summary>
    /// <returns>玩家</returns>
    private static PlayerEnum GetStartPlayer()
    {
        return PlayerEnum.Player1;
    }

    #region 检测方法
    /// <summary>
    /// 检查玩家能否行动
    /// </summary>
    /// <param name="player">玩家</param>
    /// <returns>true：可以行动，false：不可以行动</returns>
    private static bool CheckPlayerCanAction(PlayerEnum playerEnum)
    {
        PlayerBase player = playerEnum == NetworkManager.instance.playerEnum ? Myself.instance : Opponent.instance;
        if (player == null)
            return false;

        // 如果是离线，那敌人也不要行动了。如果后面有敌人AI逻辑在改吧
        if (!NetworkManager.isOnline && player == Opponent.instance)
            return false;

        return true;
    }

    /// <summary>
    /// 检查游戏是否结束了
    /// </summary>
    /// <returns>true：游戏结束，false：游戏没结束</returns>
    private static bool CheckGameOver(out PlayerEnum winner)
    {
        winner = PlayerEnum.NotReady;
        return false;
    }
    #endregion
}
