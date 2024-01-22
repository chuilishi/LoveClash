using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Script.Cards;

namespace Script.core
{
    public class Deck
    {
        private List<Card> cards;
        private view.Deck deckView;
        public static Deck instance;

        public Deck(List<Card> cards,view.Deck deckView)
        {
            instance = this;
            this.cards = cards;
            this.deckView = deckView;
        }
        public Card DrawCard()
        {
            if(cards.Count==0)return null;
            var card = cards[0];
            deckView.DrawCard(card);
            cards.RemoveAt(0);
            return card;
        }
        public List<Card> DrawCard(List<int>indexes)
        {
            indexes.Sort();
            List<Card> list = new List<Card>();
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                if(indexes[i]<0||indexes[i]>=cards.Count)continue;
                list.Add(cards[indexes[i]]);
                cards.RemoveAt(indexes[i]);
            }
            return list;
        }
    }
}