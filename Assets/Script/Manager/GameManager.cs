using System;
using System.Runtime.InteropServices;
using Script.core;
using Script.Network;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public PlayerEnum curPlayer = PlayerEnum.Player1;
        
        private void Awake()
        {
            instance = this;
        }

        #region 游戏流程

        public UnityEvent GameStart;
        public void EndTurn(PlayerEnum playerEnum)
        {
            //TODO
        }

        public void EndGame()
        {
            //TODO
        }
        #endregion
        
    }
}