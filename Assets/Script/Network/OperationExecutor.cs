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
    public class OperationExecutor : MonoBehaviour
    {
        public static Queue<Operation> _operations = new Queue<Operation>();

        #region 一些事件

        public static event Action<Card,PlayerEnum> cardEvent;
        public static event Action<PlayerEnum> endTurnEvent;
        public static event Action<IExecutable,PlayerEnum> skillEvent;

        #endregion

        private void Start()
        {
            Main();
        }
        /// <summary>
        /// 持续执行队列里的命令
        /// </summary>
        private async void Main()
        {
            while (true)
            {
                await UniTask.WaitUntil(() => _operations.Count != 0);
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
            if (!NetworkManager.isOnline)
            {
                UnityEngine.Debug.Log("offline");
                _operations.Enqueue(operation);
                return;
            }
            var task = NetworkUtility.RequestAsync(NetworkManager.instance.senderClient, JsonUtility.ToJson(operation));
            task.GetAwaiter().OnCompleted((
                () => { _operations.Enqueue(JsonUtility.FromJson<Operation>(task.GetAwaiter().GetResult())); }));
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
            endTurnEvent?.Invoke(operation.playerEnum);
        }
        private static async UniTask Card(Operation operation)
        {
            var o = (NetworkObject)operation.baseObject;
            var card = o.GetComponent<Card>();
            cardEvent?.Invoke(card,operation.playerEnum);
            if (operation.playerEnum == NetworkManager.playerEnum)
            {
                await Myself.instance.PlayCard(card,
                    operation.targetNetworkObjects);
            }
            else
            {
                await Opponent.instance.PlayCard(card,
                    operation.targetNetworkObjects);
            }
        }
        //比如抽卡
        private async UniTask Skill(Operation operation)
        {
            //C#的奇怪写法 skill 代表强制转换后的对象
            if (operation.baseObject is not IExecutable skill)
            {
                UnityEngine.Debug.LogError($"名为{operation.extraMessage}的Skill的名称错误");
                return;
            }
            skillEvent?.Invoke(skill,operation.playerEnum);
            skill.Execute(
                operation.playerEnum == NetworkManager.playerEnum ? Myself.instance : Opponent.instance,
                operation.targetNetworkObjects);
        }

        private static async UniTask CreateObject(Operation operation)
        {
            NetworkManager.InstantiateNetworkObjectLocal(operation.extraMessage,
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
                UnityEngine.Debug.Log("服务器错误");
            }
        }
    }
}