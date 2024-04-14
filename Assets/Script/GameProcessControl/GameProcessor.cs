using Script;
using Script.core;
using Script.Effects.����Effect;
using Script.Manager;
using Script.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public enum GameState
{
    GameStart, // ��Ϸ��ʼ
    StartTurn, // �غϿ�ʼ
    RunningTurn, // �غ���
    OverTurn, // �غϽ���
    GameOver // ��Ϸ����
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
        // ��ʼ����Ϸ����
        Init();

        // ��ʼ����ɾ�֪ͨ��Ϸ��ʼ����ЩЧ��������Ϸ��ʼҪ����
        _state = GameState.GameStart;
        using var evt = GameStartEvent.Get();
        EventManager.SendEvent(evt);
        Debug.Log($"��Ϸ��ʼ");

        // ����һ������˭�Ļغ�
        _curPlayer = GetStartPlayer();
        Run(GameState.StartTurn);
    }

    public static void Over()
    {
        _state = GameState.GameOver;
        var evt = GameOverEvent.Get();
        evt.winner = _winner;
        EventManager.SendEvent(evt);
        Debug.Log($"��Ϸ����");
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
        Debug.Log($"�غϿ�ʼ��{_curPlayer}");

        if (CurPlayer == NetworkManager.instance.playerEnum)
        {
            GameManager.instance.ExecuteOperation(new Operation(OperationType.Effect, extraMessage: typeof(DrawCardEffect).FullName));
        }

        if (CheckPlayerCanAction(_curPlayer))
        {
            //������ӶԶԷ������ƵĲ�ѯ
            List<ICounter> counters = Opponent.instance.GetCounterCard();

            foreach (var counter in counters)
            {
                //���� 1
                //���Ƶ����ȼ���Ϊ ��
                //EventManager.Register<PlayCardEvent>(counter.Counter, GameEventPriority.High);

                //���� 2
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
        Debug.Log($"�غ��У�{_curPlayer}");
    }

    private static void OnOverTurn()
    {
        using var evt = TurnOverEvent.Get();
        evt.playerEnum = CurPlayer;
        EventManager.SendEvent(evt);
        Debug.Log($"�غϽ�����{_curPlayer}");

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
    /// ���ؿ�ʼ�����
    /// </summary>
    /// <returns>���</returns>
    private static PlayerEnum GetStartPlayer()
    {
        return PlayerEnum.Player1;
    }

    #region ��ⷽ��
    /// <summary>
    /// �������ܷ��ж�
    /// </summary>
    /// <param name="player">���</param>
    /// <returns>true�������ж���false���������ж�</returns>
    private static bool CheckPlayerCanAction(PlayerEnum playerEnum)
    {
        PlayerBase player = playerEnum == NetworkManager.instance.playerEnum ? Myself.instance : Opponent.instance;
        if (player == null)
            return false;

        // ��������ߣ��ǵ���Ҳ��Ҫ�ж��ˡ���������е���AI�߼��ڸİ�
        if (!NetworkManager.isOnline && player == Opponent.instance)
            return false;

        return true;
    }

    /// <summary>
    /// �����Ϸ�Ƿ������
    /// </summary>
    /// <returns>true����Ϸ������false����Ϸû����</returns>
    private static bool CheckGameOver(out PlayerEnum winner)
    {
        winner = PlayerEnum.NotReady;
        return false;
    }
    #endregion
}
