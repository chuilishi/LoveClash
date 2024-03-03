using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Manager;
using Script.Network;
using Script.Skills;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Script.core
{
    public abstract class NetworkObject : MonoBehaviour,INetworkObject
    {
        public int networkId { get; set; } = -1;

        public @string String { get; set; }

        protected static async UniTask<NetworkObject> InstantiateNetworkObject(string prefabName,Transform transform = null)
        {
            return await NetworkManager.InstantiateNetworkObject(prefabName, transform);
        }
        protected static void Execute(Operation operation)
        {
            OperationExecutor.Execute(operation);
        }

        protected static void ExecuteCard(NetworkObject cardGameObject,List<NetworkObject>targets=null)
        {
            Execute(new Operation(OperationType.Card,baseObject: cardGameObject,targetNetworkObjects: targets));
        }

        protected static void ExecuteSkill(IExecutable skill,List<NetworkObject>targets=null)
        {
            Execute(new Operation(OperationType.Skill,skill,targetNetworkObjects: targets));
        }
    }
}