using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Script.Cards;
using Script.Character;
using Script.core;
using Script.Network;
using Script.Skills;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Manager
{
    [RequireComponent(typeof(OperationExecutor))]
    [RequireComponent(typeof(UIExecutor))]
    public class GameManager : NetworkObject
    {
        public static GameManager instance;
        public PlayerEnum curPlayer = PlayerEnum.Player1;
        private void Awake()
        {
            instance = this;
        }
        #region 一些事件

        public static event Action<Card,PlayerEnum> cardEvent;
        public static event Action<PlayerEnum> endTurnEvent;
        public static event Action<IExecutable,PlayerEnum> skillEvent;

        #endregion
        #region 游戏流程
        public void Main(bool isOnline = true)
        {
            if (isOnline == false)
            {
                NetworkManager.isOnline = false;
                NetworkManager.playerEnum = PlayerEnum.NotReady;
                Offline();
            }
            else
            {
                Online();
            }
        }
        /// <summary>
        /// 单机流程
        /// </summary>
        private void Offline()
        {
            Execute(new Operation(OperationType.Skill,new DrawCardSkill()));
            OperationExecutor.cardEvent += ((card, playerEnum) =>
            {
                Execute(new Operation(OperationType.Skill,new DrawCardSkill()));
            });
        }
        /// <summary>
        /// 联机流程
        /// </summary>
        private void Online()
        {
            OperationExecutor.cardEvent += (card, @enum) =>
            {
                if (@enum != NetworkManager.playerEnum)
                {
                    ExecuteSkill(new DrawCardSkill());
                }
            };
            if(NetworkManager.playerEnum==PlayerEnum.Player1)ExecuteSkill(new DrawCardSkill());
        }
        public void EndGame()
        {
            //TODO
        }
        #endregion
    }
}