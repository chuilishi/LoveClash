using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.core;
using Script.Manager;
using Script.Skills;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.Network
{
    /// <summary>
    /// 只负责执行,不关心网络
    /// </summary>
    public class OperationExecutor : NetworkObject
    {
        public static Queue<Operation> _operations = new Queue<Operation>();
        /// <summary>
        /// 持续执行队列里的命令
        /// </summary>
        private async void Main()
        {
            while (true)
            {
                await UniTask.WaitUntil(() => _operations.Count != 0);
                UnityEngine.Debug.Log("数量" + _operations.Count);
                var operation = _operations.Dequeue();
                await m_Execute(operation);
            }
        }
        /// <summary>
        /// 最常用的,外部用来执行命令的方法
        /// </summary>
        /// <param name="operation"></param>
        public static void Execute(Operation operation)
        {
            //单机
            if (NetworkManager.playerEnum == PlayerEnum.NotReady)
            {
                _operations.Enqueue(operation);
            }
            else // 联机
            {
                var task = NetworkUtility.RequestAsync(NetworkManager.instance.senderClient, JsonUtility.ToJson(operation));
                task.GetAwaiter().OnCompleted((
                    () => { _operations.Enqueue(JsonUtility.FromJson<Operation>(task.GetAwaiter().GetResult())); }));
            }
        }
        public async UniTask m_Execute(Operation operation)
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
                case OperationType.Debug:
                    await Debug(operation);
                    break;
            }
        }

        private static async UniTask EndTurn(Operation operation)
        {
            GameManager.instance.TurnChange.Invoke(operation.playerEnum == PlayerEnum.Player1
                ? PlayerEnum.Player2
                : PlayerEnum.Player1);
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
                ExecuteSkill(new DrawCardSkill());
            }
        }
        //比如抽卡
        private async UniTask Skill(Operation operation)
        {
            var type = Type.GetType(operation.extraMessage);
            if (type == null)
            {
                UnityEngine.Debug.LogError($"名为{operation.extraMessage}的Skill的名称错误");
                return;
            }
            ((IExecutable)Activator.CreateInstance(type)).Execute(
                operation.playerEnum == NetworkManager.playerEnum ? Player.instance : Opponent.instance,
                operation.targetNetworkObjects);
        }

        private static async UniTask CreateObject(Operation operation)
        {
            NetworkManager.InstantiateNetworkObjectLocal((ObjectEnum)int.Parse(operation.extraMessage),
                operation.baseNetworkId, UIManager.instance.物品池.transform);
        }

        private static async UniTask Debug(Operation operation)
        {
            UnityEngine.Debug.Log("DEBUG message from Player" + operation.playerEnum + ": " + operation.extraMessage);
        }

        private static async UniTask Error(Operation operation)
        {
            if (operation.playerEnum == NetworkManager.playerEnum)
            {
                
            }
        }
    }
}