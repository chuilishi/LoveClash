using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Script.core;
using Script.Manager;
using UnityEditor;
using UnityEngine;

namespace Script.Network
{
    /// <summary>
    /// operation 中的所有东西都能
    /// </summary>
    [Serializable]
    public class Operation : ISerializationCallbackReceiver
    {
        public OperationType operationType;
        public int operationId = 0;
        public PlayerEnum playerEnum;
        public int baseNetworkId = -1;
        public List<int> targetNetworkId = new List<int>();
        [NonSerialized]
        public NetworkObject baseNetworkObject;
        [NonSerialized]
        public List<NetworkObject> targetNetworkObjects;
        /// <summary>
        /// 一些额外的附加信息 比如connect时对方的用户名
        /// </summary>
        public string extraMessage;
        public Operation(OperationType operationType = OperationType.Error,string extraMessage=null,NetworkObject baseNetworkObject = null,List<NetworkObject> targetNetworkObjects=null)
        {
            this.operationType = operationType;
            baseNetworkId = baseNetworkObject == null ? -1 : baseNetworkObject.networkId;
            if (targetNetworkObjects != null)
            {
                foreach (var o in targetNetworkObjects)
                {
                    targetNetworkId.Add(o.networkId);
                }
            }
            this.extraMessage = extraMessage;
            playerEnum = NetworkManager.playerEnum;
        }
        public void OnBeforeSerialize()
        {
            
        }
        public void OnAfterDeserialize()
        {
            baseNetworkObject = NetworkManager.GetObjectById(baseNetworkId);
            targetNetworkObjects = new List<NetworkObject>();
            foreach (var id in targetNetworkId)
            {
                targetNetworkObjects.Add(NetworkManager.GetObjectById(id));
            }
        }
    }
}