using System.Collections.Generic;
using Script.core;

namespace Script.Skills
{
    public class RandomCardSkill : IExecutable
    {
        public void Execute(PlayerBase playerBase, List<NetworkObject> targets = null)
        {
            playerBase.DrawCard();
        }
    }
}