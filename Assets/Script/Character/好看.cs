using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.core;

namespace Script.Character
{
    public class 好看 : CharacterBase
    {
        public override void PlayCard(Card card, List<NetworkObject> targets)
        {
            if (card is BaseCardBase)
            {
                GetComponent<PlayerBase>().心动值 += 1;
            }
            card.Execute(GetComponent<PlayerBase>(),targets);
        }
    }
}