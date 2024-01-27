using System;
using System.Collections.Generic;
using Script.core;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Cards
{
    [Serializable]
    public abstract class Card : NetworkObject
    {
        public Sprite cardImage;
        [HideInInspector]
        public view.CardView cardView;

        private void Awake()
        {
            cardView = GetComponent<view.CardView>();
            Assert.AreNotEqual(cardView,null);
            transform.Find("Picture").GetComponent<Image>().sprite = cardImage;
        }

        public abstract void Execute(List<NetworkObject> targets = null);
    }
}