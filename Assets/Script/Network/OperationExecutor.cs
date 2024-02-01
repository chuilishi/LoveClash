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
        private void Awake()
        {
           Main();
        }

        public static async void Main()
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
                case OperationType.TryConnectRoom:
                    await ConnectHandler(operation);
                    break;
                case OperationType.EndTurn:
                    await EndTurn(operation);
                    break;
            }
        }

        private static async UniTask EndTurn(Operation operation)
        {
            //TODO
            
        }
        private static async UniTask Card(Operation operation)
        {
            operation.baseNetworkObject.GetComponent<Card>().Execute(operation.targetNetworkObjects);
        }

        private static async UniTask Skill(Operation operation)
        {
            
        }

        private static async UniTask ConnectHandler(Operation operation)
        {
               
        }
    }
}