using System;
using System.Collections.Generic;
using Script.view;
using UnityEngine;

namespace Script
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField]
        private GenericDictionary<int, Card> cards;

        private static GenericDictionary<int, Card> _cards;
        private void Awake()
        {
            _cards = cards;
            DontDestroyOnLoad(gameObject);
        }
        public static Card GetCard(int num)
        {
            if (num >= _cards.Count || num < 0) return null;
            return Instantiate(_cards[num]);
        }
    }
}