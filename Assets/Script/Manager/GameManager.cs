using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Script.core;
using Script.Network;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Manager
{
    [RequireComponent(typeof(OperationExecutor))]
    [RequireComponent(typeof(UIExecutor))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public PlayerEnum curPlayer = PlayerEnum.Player1;
        private void Awake()
        {
            instance = this;
            EditorApplication.playModeStateChanged += (change =>
            {
                if (change == PlayModeStateChange.ExitingPlayMode)
                {
                    NetworkManager.CloseAll();
                }
            });
        }
        public UnityEvent GameStart;
        //参数代表目前玩家是谁
        public UnityEvent<PlayerEnum> TurnChange;
        #region 游戏流程
        public async void Main()
        {
            instance.GameStart.Invoke();
            TurnChange.AddListener((player => { curPlayer = player;}));
        }
        public void EndGame()
        {
            //TODO
            
        }
        #endregion
        
    }
}