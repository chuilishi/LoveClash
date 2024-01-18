using System.Collections.Generic;
using System.Linq;

namespace Script.core
{
    public class Deck
    {
        public List<Card> curCards = new List<Card>();
        private view.Deck deckView;

        public void Init(view.Deck deckView)
        {
            curCards = deckView.curCards.Select((card => card.card)).ToList();
            this.deckView = deckView;
        }
        public Card DrawCard()
        {
            if(curCards.Count==0)return null;
            var card = curCards[^1];
            curCards.RemoveAt(curCards.Count-1);
            return card;
        }
    }
}