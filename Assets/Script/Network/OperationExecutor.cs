using System;
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
        public static OperationExecutor instance;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// 最常用的,外部用来执行命令的方法
        /// </summary>
        /// <param name="operation"></param>
        public void Execute(Operation operation)
        {
            if (!NetworkManager.isOnline)
            {
                m_Execute(operation);
                return;
            }
            var task = NetworkUtility.RequestAsync(NetworkManager.instance.senderClient, JsonUtility.ToJson(operation));
            task.GetAwaiter().OnCompleted(() =>
            {
                m_Execute(JsonUtility.FromJson<Operation>(task.GetAwaiter().GetResult()));
            });
        }
        
        public void m_Execute(Operation operation)
        {
            switch (operation.operationType)
            {
                case OperationType.Card:
                    Card(operation);
                    break;
                case OperationType.EndTurn:
                    EndTurn(operation);
                    break;
                case OperationType.CreateObject:
                    CreateObject(operation);
                    break;
                case OperationType.Debug:
                    Debug(operation);
                    break;
                case OperationType.Effect:
                    Effect(operation);
                    break;
            }
        }

        private static void EndTurn(Operation operation)
        {
            GameProcessor.TurnOver(operation.playerEnum);
        }
        private static void Card(Operation operation)
        {
            var card = operation.baseNetworkId;

            using var playCardEvent = PlayCardEvent.Get();
            playCardEvent.playerEnum = operation.playerEnum;
            playCardEvent.cardId = card;
            EventManager.SendEvent(playCardEvent);

            if (operation.playerEnum == NetworkManager.instance.playerEnum)
            {
                Myself.instance.PlayCard(card,
                    operation.targetNetworkIds);
            }
            else
            {
                Opponent.instance.PlayCard(card,
                    operation.targetNetworkIds);
            }
        }
        private static void CreateObject(Operation operation)
        {
            NetworkManager.InstantiateNetworkObjectLocal(operation.extraMessage,
                operation.baseNetworkId, UIManager.instance.物品池.transform);
        }

        private static void Debug(Operation operation)
        {
            UnityEngine.Debug.Log("DEBUG message from Player" + operation.playerEnum + ": " + operation.extraMessage);
        }

        private static void Error(Operation operation)
        {
            if (operation.playerEnum == NetworkManager.instance.playerEnum)
            {
                UnityEngine.Debug.Log("服务器错误");
            }
        }
        private void Effect(Operation operation)
        {
            if (operation.playerEnum == NetworkManager.instance.playerEnum)
            {
                var type = Type.GetType(operation.extraMessage);
                if (type == null) return;
                var effect = (EffectBase)gameObject.GetComponent(type);
                if (effect == null)
                {
                    effect = (EffectBase)gameObject.AddComponent(type);
                }
                effect.Trigger(operation.targetNetworkIds);
            }
        }
    }
}