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
    public class Operation
    {
        public OperationType operationType;
        public int operationId = 0;
        public PlayerEnum playerEnum;
        public int baseNetworkId = -1;
        public List<int> targetNetworkIds;
        /// <summary>
        /// 一些额外的附加信息 比如connect时对方的用户名
        /// </summary>
        public string extraMessage;
        public Operation(OperationType operationType,int baseNetworkId = -1,List<int> targetNetworkIds = null,string extraMessage = null)
        {
            playerEnum = NetworkManager.instance.playerEnum;
            this.operationType = operationType;
            this.baseNetworkId = baseNetworkId;
            this.targetNetworkIds = targetNetworkIds;
            this.extraMessage = extraMessage;
        }
    }
}