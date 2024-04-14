using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Script.Cards;
using Script.Character;
using Script.core;
using Script.Effects.其他Effect;
using Script.Network;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Manager
{
    [RequireComponent(typeof(OperationExecutor))]
    public class GameManager : NetworkObject
    {
        public static GameManager instance;
        public PlayerEnum curPlayer = PlayerEnum.Player1;

        private void Awake()
        {
            instance = this;
        }
        #region 游戏流程
        public void Main(bool isOnline = true)
        {
            if (isOnline == false)
            {
                NetworkManager.isOnline = false;
                NetworkManager.instance.playerEnum = PlayerEnum.Player1; // 单机自己就是一号玩家
            }

            GameProcessor.Start();
        }

        public void EndGame()
        {
            //TODO
        }
        #endregion

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

        /// <summary>
        /// 给非网络对象用的发包接口
        /// </summary>
        /// <param name="operation"></param>
        public void ExecuteOperation(Operation operation)
        {
            Execute(operation);
        }
    }
}