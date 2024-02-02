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
    public class Operation : ISerializationCallbackReceiver
    {
        public OperationType operationType;
        public int operationId = 0;
        public PlayerEnum playerEnum;
        public NetworkObject baseNetworkObject = null;
        public List<NetworkObject> targetNetworkObjects;

        /// <summary>
        /// 一些额外的附加信息 比如connect时对方的用户名
        /// </summary>
        public string extraMessage;
        public Operation(OperationType operationType = OperationType.Error,PlayerEnum playerEnum = PlayerEnum.NotReady,string extraMessage=null,NetworkObject baseNetworkObject = null,List<NetworkObject> targetNetworkObjects=null)
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
            if (baseNetworkObject != null)
            {
                var o = NetworkManager.GetObjectById(baseNetworkObject.networkId);
                if (o == null)
                {
                    var id = baseNetworkObject.networkId;
                    baseNetworkObject = ObjectFactory.instance.GetObject(baseNetworkObject.name);
                    baseNetworkObject.networkId = id;
                    NetworkManager.networkObjects.Add(baseNetworkObject.networkId,baseNetworkObject);
                }
                else
                {
                    baseNetworkObject = o;
                }
            }

            if (targetNetworkObjects != null)
            {
                for (var index = 0; index < targetNetworkObjects.Count; index++)
                {
                    var oo = NetworkManager.GetObjectById(targetNetworkObjects[index].networkId);
                    if (oo == null)
                    {
                        var id = targetNetworkObjects[index].networkId;
                        targetNetworkObjects[index] = ObjectFactory.instance.GetObject(targetNetworkObjects[index].name);
                        targetNetworkObjects[index].networkId = id;
                        NetworkManager.networkObjects.Add(targetNetworkObjects[index].networkId,targetNetworkObjects[index]);
                    }
                    else
                    {
                        targetNetworkObjects[index] = oo;
                    }
                }
            }
        }
    }
}