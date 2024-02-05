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
        [NonSerialized]
        public NetworkObject baseNetworkObject = null;
        [NonSerialized]
        public List<NetworkObject> targetNetworkObjects;
        
        public string baseNetworkObjectJson;
        public List<string> targetNetworkObjectsJson;
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
            if(baseNetworkObject!=null) baseNetworkObjectJson = JsonUtility.ToJson(baseNetworkObject);
            if (targetNetworkObjects != null)
            {
                foreach (var o in targetNetworkObjects)
                {
                    targetNetworkObjectsJson.Add(JsonUtility.ToJson(o));
                }
            }
        }
        public void OnAfterDeserialize()
        {
            try
            {
                if (!string.IsNullOrEmpty(baseNetworkObjectJson))
                {
                    var json = JsonUtility.FromJson<NetworkObjectJson>(baseNetworkObjectJson);
                    Debug.Log(baseNetworkObjectJson);
                    if (json.networkId != -1)
                    {
                        var o = NetworkManager.GetObjectById(json.networkId);
                        if (o == null)
                        {
                            o = NetworkManager.InstantiateNetworkObjectLocal(json.objectEnum, json.networkId,
                                UIManager.instance.物品池.transform);
                            Debug.Log(o.name);
                        }
                        baseNetworkObject = o;
                    }
                }

                if (targetNetworkObjectsJson != null)
                {
                    for (var index = 0; index < targetNetworkObjectsJson.Count; index++)
                    {
                        if (!string.IsNullOrEmpty(targetNetworkObjectsJson[index]))
                        {
                            var json = JsonUtility.FromJson<NetworkObjectJson>(targetNetworkObjectsJson[index]);
                            if (json.networkId != -1)
                            {
                                var o = NetworkManager.GetObjectById(json.networkId);
                                if (o == null)
                                {
                                    o = NetworkManager.InstantiateNetworkObjectLocal(json.objectEnum, json.networkId,
                                        UIManager.instance.物品池.transform);
                                }
                                targetNetworkObjects.Add(o);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}