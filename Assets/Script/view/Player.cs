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
        public static Player instance;
        public core.Player player;
        public Deck deckView;
        
        private List<Card> handCards = new List<Card>();
        private TMP_Text 心动值;
        private TMP_Text 信任值;
        private TMP_Text 上头值;
        private void Awake()
        {
            player = new core.Player();
            instance = this;
            心动值 = transform.Find("心动值/心动值Text").gameObject.GetComponent<TextMeshProUGUI>();
            上头值 = transform.Find("上头值/上头值Text").gameObject.GetComponent<TextMeshProUGUI>();
            信任值 = transform.Find("信任值/信任值Text").gameObject.GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            player.Init(this,deckView.deck,(int.Parse(心动值.text),int.Parse(信任值.text),int.Parse(上头值.text)));
        }
        [Button]
        public void DrawCard()
        {
            player.DrawCard();
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
        //anchoredPosition为单位的间隔
        private float interval;
        private void AdjustPos()
        {
            try
            {
                if (handCards.Count == 0) return;
                for (int i = 0; i < handCards.Count; i++)
                {
                    var minus = i - (handCards.Count-1)/2f;
                    handCards[i].ResetPosition(new Vector3(minus*UIManager.instance.cardInterval+UIManager.instance.CenterCardPivot.position.x,UIManager.instance.CenterCardPivot.position.y));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
