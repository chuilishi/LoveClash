using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
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
        public UnityEvent GameStart;
        //参数代表目前玩家是谁
        public UnityEvent<PlayerEnum> TurnChange;
        #region 游戏流程
        public void Main()
        {
            if (NetworkManager.playerEnum == PlayerEnum.NotReady)
            {
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
            
        }
        /// <summary>
        /// 联机流程
        /// </summary>
        private void Online()
        {
            if(NetworkManager.playerEnum==PlayerEnum.Player1)ExecuteSkill(new DrawCardSkill());
        }
        public void EndGame()
        {
            //TODO
        }
        #endregion
    }
}