using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Manager;
using Script.Network;
using UnityEngine;
using UnityEngine.Assertions;

namespace Script.core
{
    public class Deck : NetworkObject
    {
        [Header("牌库中的卡")]
        public List<string> cards;

        private List<CardBase> m_cards = new List<CardBase>();
        public static Deck instance;

        private void Awake()
        {
            instance = this;
        }

        public async UniTask Init()
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var o = await InstantiateNetworkObject(cards[i],UIManager.instance.CardsParent);
                m_cards.Add(o.GetComponent<CardBase>());
            }
        }
        public CardBase DrawCard()
        {
            if(m_cards.Count==0)return null;
            var card = m_cards[0];
            cards.RemoveAt(0);
            return card;
        }
        public List<CardBase> DrawCard(List<int>indexes)
        {
            indexes.Sort();
            List<CardBase> list = new List<CardBase>();
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                if(indexes[i]<0||indexes[i]>=cards.Count)continue;
                list.Add(m_cards[indexes[i]]);
                cards.RemoveAt(indexes[i]);
            }
            return list;
        }
    }
}