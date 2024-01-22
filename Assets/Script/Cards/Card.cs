using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Cards
{
    [Serializable]
    public abstract class Card : MonoBehaviour
    {
        public Sprite cardImage;
        [FormerlySerializedAs("cardViewView")] [HideInInspector]
        public view.CardView cardView;

        private void Awake()
        {
            cardView = GetComponent<view.CardView>();
            Assert.AreNotEqual(cardView,null);
            transform.Find("Picture").GetComponent<Image>().sprite = cardImage;
        }

        public abstract void Execute(Card target = null);
    }
}