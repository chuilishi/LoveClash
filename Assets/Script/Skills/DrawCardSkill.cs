using System.Collections.Generic;
using Script.core;
using Script.Network;
using UnityEngine;

namespace Script.Skills
{
    public class DrawCardSkill : IExecutable 
    {
        public void Execute(PlayerBase playerBase, List<NetworkObject> targets = null)
        {
            Debug.Log("DrawcardSkill 执行了");
            playerBase.DrawCard();
        }
    }
}