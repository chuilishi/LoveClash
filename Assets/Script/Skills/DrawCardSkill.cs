using System.Collections.Generic;
using Script.core;
using Script.Network;

namespace Script.Skills
{
    public class DrawCardSkill : IExecutable
    {
        public void Execute(Character character, List<NetworkObject> targets = null)
        {
            character.DrawCard();
        }
    }
}