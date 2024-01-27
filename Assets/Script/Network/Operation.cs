using System;
using System.Collections.Generic;
using Script.core;
using UnityEngine;

namespace Script.Network
{
    /// <summary>
    /// operation 中的所有东西都能
    /// </summary>
    [Serializable]
    public class Operation
    {
        public OperationType operationType;
        
        public int operationId = 0;
        public PlayerEnum playerEnum;
        public NetworkObject baseNetworkObject;
        public List<NetworkObject> targetNetworkObjects;

        /// <summary>
        /// 一些额外的附加信息 比如connect时对方的用户名
        /// </summary>
        public string extraMessage = null;
        public Operation(OperationType operationType,PlayerEnum playerEnum = PlayerEnum.NotReady,NetworkObject baseNetworkObject = null,List<NetworkObject> targetNetworkObjects=null,string extraMessage=null)
        {
            this.operationType = operationType;
            this.playerEnum = playerEnum;
            this.baseNetworkObject = baseNetworkObject;
            this.targetNetworkObjects = targetNetworkObjects;
            this.extraMessage = extraMessage;
        }
        
        public void OnBeforeSerialize()
        {
        }
        public void OnAfterDeserialize()
        {
            var o = NetworkManager.GetObjectById(baseNetworkObject.networkId);
            baseNetworkObject = o == null ? NetworkManager.CreateNewNetworkObject(baseNetworkObject.name,baseNetworkObject.networkId) : o;

            for (var index = 0; index < targetNetworkObjects.Count; index++)
            {
                var target = NetworkManager.GetObjectById(targetNetworkObjects[index].networkId);
                if (target == null)
                {
                    targetNetworkObjects[index] = NetworkManager.CreateNewNetworkObject(targetNetworkObjects[index].name);
                }
                else
                {
                    targetNetworkObjects[index] = target;
                }
            }
        }
    }

    public enum OperationType
    {
        Create,
        Card,
        Skill,
        ConnectRoom,
        CreateRoom,
        GameStart
    }
}