using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EasyButtons;
using Script.Manager;
using TMPro;
using UnityEngine;

namespace Script.view
{
    public class PlayerView : CharacterView
    {
        public override void Awake()
        {
            base.Awake();
            UIManager.instance.playerView = this;
        }
        /// <summary>
        /// 抽Cards.Card入手牌
        /// </summary>
        /// <param name="cards"></param>
        public void DrawCard(List<Cards.CardBase>cards)
        {
            if (cards.Count == 0)
            {
                Debug.Log("没牌了");
                return;
            }
            foreach (var card in cards)
            {
                handCards.Add(card.cardView);
            }
            AdjustPos();
        }
        public void DrawCard(CardView cardView)
        {
            try
            {
                handCards.Add(cardView);
                AdjustPos();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        //anchoredPosition为单位的间隔
        private float interval;
        private void AdjustPos()
        {
            if (handCards.Count == 0) return;
            for (int i = 0; i < handCards.Count; i++)
            {
                var minus = i - (handCards.Count-1)/2f;
                handCards[i].ResetPosition("adjustpos",new Vector3(minus*UIManager.instance.cardInterval+UIManager.instance.centerCardPivot.position.x,UIManager.instance.centerCardPivot.position.y));
            }
        }
    }
}
