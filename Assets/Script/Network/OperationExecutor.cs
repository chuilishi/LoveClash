using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.core;
using Script.Manager;
using UnityEngine;

namespace Script.Network
{
    /// <summary>
    /// 只负责执行,不关心网络
    /// </summary>
    public class OperationExecutor : MonoBehaviour
    {
        public static List<Operation> _operations = new List<Operation>();
        private void Awake()
        {
           Main();
        }
        /// <summary>
        /// 持续执行队列里的命令
        /// </summary>
        private static async void Main()
        {
            while (true)
            {
                await UniTask.WaitUntil(()=>_operations.Count != 0);
                var operation = _operations[0];
                _operations.RemoveAt(0);
                await m_Execute(operation);
            }
        }
        /// <summary>
        /// 最常用的,外部用来执行命令的方法
        /// </summary>
        /// <param name="operation"></param>
        public static void Execute(Operation operation)
        {
            var task = NetworkUtility.RequestAsync(NetworkManager.senderClient,JsonUtility.ToJson(operation));
            task.GetAwaiter().OnCompleted((
                () =>
                {
                    _operations.Add(JsonUtility.FromJson<Operation>(task.GetAwaiter().GetResult()));
                }));
        }
        private static async UniTask m_Execute(Operation operation)
        {
            switch (operation.operationType)
            {
                case OperationType.Card:
                    await Card(operation);
                    break;
                case OperationType.Skill:
                    await Skill(operation);
                    break;
                case OperationType.EndTurn:
                    await EndTurn(operation);
                    break;
                case OperationType.CreateObject:
                    await CreateObject(operation);
                    break;
            }
        }
        private static async UniTask EndTurn(Operation operation)
        {
            GameManager.instance.TurnChange.Invoke(operation.playerEnum==PlayerEnum.Player1?PlayerEnum.Player2: PlayerEnum.Player1);
        }
        private static async UniTask Card(Operation operation)
        {
            if (operation.playerEnum == NetworkManager.playerEnum)
            {
                await Player.instance.PlayCard(operation.baseNetworkObject.GetComponent<Card>(),
                    operation.targetNetworkObjects);
            }
            else
            {
                await Opponent.instance.PlayCard(operation.baseNetworkObject.GetComponent<Card>(),
                    operation.targetNetworkObjects);
            }
        } 
        //比如抽卡
        private static async UniTask Skill(Operation operation)
        {
            
        }
        private static async UniTask CreateObject(Operation operation)
        {
            if (operation.playerEnum == NetworkManager.playerEnum) return;//其实这个不应该出现的,因为CreateNetworkObject 用的是Request
        }
    }
}