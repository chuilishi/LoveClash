using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.core;

namespace Script.Character
{
    public class 好看 : CharacterBase
    {
        public override void PlayCard(CardBase cardBase, List<NetworkObject> targets)
        {
            if (cardBase is BaseCardBase)
            {
                GetComponent<PlayerBase>().心动值 += 1;
            }
            cardBase.Execute(GetComponent<PlayerBase>(),targets);
        }
    }
}