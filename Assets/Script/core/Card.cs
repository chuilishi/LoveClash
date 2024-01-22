using System;
using UnityEngine;

namespace Script.Cards
{
    [Serializable]
    public abstract class Card
    {
        public view.Card cardView;

        public Card(view.Card cardView)
        {
            this.cardView = cardView;
        }

        public abstract void Execute(Card target = null);
    }
}