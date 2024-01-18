using System;
using System.Collections.Generic;
using Script.Utility;
using Script.view;
using UnityEngine;

namespace Script
{
    public class CardFactory : MonoBehaviour
    {
        [SerializeField]
        private GenericDictionary<int, Card> dict;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        public static Card GetCard(int num)
        {
            // if (num >= _cards.Count || num < 0) return null;
            // return Instantiate(_cards[num]);
            return null;
        }
    }
}