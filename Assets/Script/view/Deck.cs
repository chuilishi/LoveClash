using System;
using System.Collections.Generic;
using System.Linq;
using Script.core;
using Script.Manager;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace Script.view
{
    public class Deck : MonoBehaviour
    {
        public core.Deck deck;
        //卡组
        public CardGroupSO cardGroupSo;
        private void Awake()
        {
            List<Cards.Card> list = new List<Cards.Card>();
            foreach (var card in cardGroupSo.cards)
            {
                var cardView = Instantiate(card, transform.position, Quaternion.identity, UIManager.instance.CardsParent);
                list.Add(cardView.card);
            }
            deck = new core.Deck(list,this);
        }
        private void Start()
        {
            
        }
        public Card DrawCard(Cards.Card card)
        {
            if (card == null) return null;
            return card.cardView;
        }
    }
}