using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Script.core;
using Script.Network;

namespace Script.Cards
{
    /// <summary>
    /// 上头值+1
    /// </summary>
    [Serializable]
    public class 喷香水 : Card
    {
        public 喷香水() : base(){}

        public override void Execute(List<NetworkObject> targets = null)
        {
            Player.instance.上头值++;
            OperationExecutor.Execute(new Operation(OperationType.Card, NetworkManager.playerEnum, baseNetworkObject:this,
                targetNetworkObjects:targets));
        }
    }
}