using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Manager;
using Script.Network;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Script.core
{
    public abstract class NetworkObject : MonoBehaviour,INetworkObject
    {
        public int networkId { get; set; } = -1;

        public ObjectEnum objectEnum { get; set; }

        protected static async UniTask<NetworkObject> InstantiateNetworkObject(ObjectEnum objectEnum,Transform transform = null)
        {
            return await NetworkManager.InstantiateNetworkObject(objectEnum, transform);
        }
        protected static void Execute(Operation operation)
        {
            OperationExecutor.Execute(operation);
        }

        protected static void ExecuteCard(Card card,List<NetworkObject>targets=null)
        {
            Execute(new Operation(OperationType.Card,baseObject: card,targetNetworkObjects: targets));
        }

        protected static void ExecuteSkill(IExecutable skill,List<NetworkObject>targets=null)
        {
            Execute(new Operation(OperationType.Skill,targetNetworkObjects: targets,extraMessage:skill.GetType().FullName));
        }
    }
}