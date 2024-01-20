using UnityEngine;

namespace Script.Cards
{
    public abstract class Card
    {
        public view.Card cardView;

        public Card(view.Card cardView)
        {
            this.cardView = cardView;
        }
    }
}