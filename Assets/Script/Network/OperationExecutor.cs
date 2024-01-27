using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Manager;
using UnityEngine;

namespace Script.Network
{
    /// <summary>
    /// 只负责执行,不关心网络
    /// </summary>
    public class OperationExecutor : MonoBehaviour
    {
        private static List<Operation> _operations = new List<Operation>();
        private async void Awake()
        {
            while (true)
            {
                await UniTask.WaitUntil(()=>_operations.Count != 0);
                var operation = _operations[0];
                _operations.RemoveAt(0);
                await m_Execute(operation);
            }
        }

        public static void Execute(Operation operation)
        {
            _operations.Add(operation);
        }

        public static void Execute(string operation)
        {
            Execute(JsonUtility.FromJson<Operation>(operation));
        }

        private async UniTask m_Execute(Operation operation)
        {
            switch (operation.operationType)
            {
                case OperationType.Create:
                    Create(operation);
                    break;
                case OperationType.Card:
                    await Card(operation);
                    break;
                case OperationType.Skill:
                    await Skill(operation);
                    break;
                case OperationType.ConnectRoom:
                    await AnotherPlayerConnect(operation);
                    break;
            }
        }
        /// <summary>
        /// 仅仅只是在networkobjects中添加东西
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        private void Create(Operation operation)
        {
            //在命令中就实现了Create
        }

        private async UniTask Card(Operation operation)
        {
            //TODO
            operation.baseNetworkObject.GetComponent<Card>().Execute(operation.targetNetworkObjects);
        }

        private async UniTask Skill(Operation operation)
        {
            
        }

        private async UniTask AnotherPlayerConnect(Operation operation)
        {
            UIManager.instance.通知板.gameObject.SetActive(true);
            UIManager.instance.通知板.text = "Connected! Opponent is "+operation.extraMessage;
        }
    }
}