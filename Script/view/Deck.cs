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
        [FormerlySerializedAs("cardSo")] public CardGroupSO cardGroupSo;
        public List<Card> curCards;
        private void Awake()
        {
            foreach (var card in cardGroupSo.cards)
            {
                 curCards.Add(Instantiate(card, transform.position, Quaternion.identity, transform));
            }
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