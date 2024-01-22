using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Manager;
using UnityEngine;

namespace Script.core
{
    public class Deck : MonoBehaviour
    {
        public List<Card> cards;
        public static Deck instance;

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            for (var i = 0; i < cards.Count; i++)
            {
                cards[i] = Instantiate(cards[i], UIManager.instance.CardsParent);
            }
        }

        public Card DrawCard()
        {
            if(cards.Count==0)return null;
            var card = cards[0];
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