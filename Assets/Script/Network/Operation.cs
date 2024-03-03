using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Script.core;
using Script.Manager;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace Script.Network
{
    [Serializable]
    public class Operation : ISerializationCallbackReceiver
    {
        public OperationType operationType;
        public int operationId = 0;
        public PlayerEnum playerEnum;
        public int baseNetworkId = -1;
        public List<int> targetNetworkId = new List<int>();
        [NonSerialized]
        public Object baseObject;
        [NonSerialized]
        public List<NetworkObject> targetNetworkObjects;
        /// <summary>
        /// 一些额外的附加信息 比如connect时对方的用户名
        /// </summary>
        public string extraMessage;
        public Operation(OperationType operationType = OperationType.Error,Object baseObject = null,List<NetworkObject> targetNetworkObjects=null,string extraMessage=null)
        {
            var no = baseObject as NetworkObject;
            baseNetworkId = no == null ? -1 : no.networkId;
            this.baseObject = baseObject;
            this.operationType = operationType;
            if (targetNetworkObjects != null)
            {
                foreach (var o in targetNetworkObjects)
                {
                    targetNetworkId.Add(o.networkId);
                }
            }
            playerEnum = NetworkManager.playerEnum;
            //单独处理skill *注意如果自己定义了extraMessage会破坏skill的自动反序列化*
            if (operationType == OperationType.Skill)
            {
                if(!string.IsNullOrEmpty(extraMessage)) Debug.LogError("Skill时候不能额外携带extraMessage,已经被覆盖");
                this.extraMessage = this.baseObject.GetType().FullName;
            }
            else
            {
                this.extraMessage = extraMessage;
            }
        }
        public void OnBeforeSerialize()
        {
            
        }
        public void OnAfterDeserialize()
        {
            if (operationType == OperationType.Skill)
            {
                var type = Type.GetType(extraMessage);
                if(type==null) Debug.LogError($"Skill: {type} 的名称错误");
                else baseObject = Activator.CreateInstance(type);
            }
            else
            {
                baseObject = NetworkManager.GetObjectById(baseNetworkId);
            }
            targetNetworkObjects = new List<NetworkObject>();
            foreach (var id in targetNetworkId)
            {
                targetNetworkObjects.Add(NetworkManager.GetObjectById(id));
            }
        }
    }
}