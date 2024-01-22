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
    public class Player : MonoBehaviour
    {
        private List<CardView> handCards = new List<CardView>();
        
        public int _心动值_ = 10;
        public int _信任值_ = 10;
        public int _上头值_ = 10;
        
        [HideInInspector]
        public TMP_Text 心动值;
        [HideInInspector]
        public TMP_Text 信任值;
        [HideInInspector]
        public TMP_Text 上头值;
        private void Awake()
        {
            UIManager.instance.player = this;
            心动值 = transform.Find("心动值/心动值Text").gameObject.GetComponent<TextMeshProUGUI>();
            上头值 = transform.Find("上头值/上头值Text").gameObject.GetComponent<TextMeshProUGUI>();
            信任值 = transform.Find("信任值/信任值Text").gameObject.GetComponent<TextMeshProUGUI>();
        }
        /// <summary>
        /// 抽Cards.Card入手牌
        /// </summary>
        /// <param name="cards"></param>
        public void DrawCard(List<Cards.Card>cards)
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

        public void DrawCard(Cards.Card card)
        {
            try
            {
                handCards.Add(card.cardView);
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
                handCards[i].ResetPosition(new Vector3(minus*UIManager.instance.cardInterval+UIManager.instance.centerCardPivot.position.x,UIManager.instance.centerCardPivot.position.y));
            }
        }
    }
}
