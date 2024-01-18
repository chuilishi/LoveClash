using System;
using System.Collections.Generic;
using System.Linq;
using Script.core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.view
{
    public class Deck : MonoBehaviour
    {
        public core.Deck deck;
        //卡组
        public CardSO cardSo;
        public List<Card> curCards;
        private void Awake()
        {
            curCards = cardSo.cards.Select((i)=>
            {
                var card = CardFactory.GetCard(i);
                deck.curCards.Add(card.card);
                card.transform.parent = transform;
                card.transform.position = transform.position;
                return card;
            }).ToList();
            deck.Init(this);
        }
        public Card DrawCard()
        {
            return deck.DrawCard().cardView;
        }

        /// <summary>
        /// 抽第几张
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Card DrawCard(int index)
        {
            if (index >= curCards.Count) return null;
            return curCards[^index];
        }
    }
}