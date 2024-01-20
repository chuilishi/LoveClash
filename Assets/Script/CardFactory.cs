using System;
using System.Collections.Generic;
using CareBoo.Serially;
using Script.view;
using UnityEngine;
using Card = Script.Cards.Card;

namespace Script
{
    public class CardFactory : MonoBehaviour
    {
        public static CardFactory instance;
        private Type type;
        [TypeFilter(typeof(Card))]
        public GenericDictionary<int,SerializableType> cards = new GenericDictionary<int, SerializableType>();
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
}