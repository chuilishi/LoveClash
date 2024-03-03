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
    public abstract class CardBase : NetworkObject,IExecutable
    {
        public Sprite cardImage;
        [HideInInspector]
        public view.CardView cardView;

        protected virtual void Awake()
        {
            cardView = GetComponent<view.CardView>();
            transform.Find("Picture").GetComponent<Image>().sprite = cardImage;
        }
        public abstract void Execute(core.PlayerBase player,List<NetworkObject> targets = null);
    }
}