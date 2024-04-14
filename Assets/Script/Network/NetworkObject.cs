using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Script.Cards;
using Script.Manager;
using Script.Network;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Script.core
{
    [RequireComponent(typeof(NetworkId))]
    public abstract class NetworkObject : MonoBehaviour
    {
        public int networkId
        {
            set => GetComponent<NetworkId>().networkId = value;
            get => GetComponent<NetworkId>().networkId;
        }
        protected static async UniTask<NetworkObject> InstantiateNetworkObject(string prefabName,Transform transform = null)
        {
            return await NetworkManager.InstantiateNetworkObject(prefabName, transform);
        }
        protected static void Execute(Operation operation)
        {
            OperationExecutor.instance.Execute(operation);
        }
        [Button]
        public void log()
        {
            Debug.Log(GetComponent<NetworkId>().networkId);
        }
    }
}