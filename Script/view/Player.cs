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
        public List<Card> handCards = new List<Card>();
        private TMP_Text 心动值;
        private TMP_Text 信任值;
        private TMP_Text 上头值;
        [Header("卡牌的出生位置")]
        private Transform cardsParent;

        private void Awake()
        {
            player = new core.Player();
            instance = this;
            cardsParent = transform.Find("Cards");
            心动值 = transform.Find("心动值/心动值Text").gameObject.GetComponent<TextMeshProUGUI>();
            上头值 = transform.Find("上头值/上头值Text").gameObject.GetComponent<TextMeshProUGUI>();
            信任值 = transform.Find("信任值/信任值Text").gameObject.GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            player.Init(this,deckView.deck,handCards.Select((card => card.card)).ToList(),(int.Parse(心动值.text),int.Parse(信任值.text),int.Parse(上头值.text)));
        }
        [Button]
        public void DrawCard()
        {
            player.DrawCard();
        }
        
        /// <summary>
        /// 抽卡
        /// </summary>
        /// <param name="num">抽多少</param>
        public void DrawCard(List<core.Card>cards)
        {
            if (cards.Count == 0) return;
            cards.ForEach((card => handCards.Add(card.cardView)));
            AdjustPos();
        }
        //anchoredPosition为单位的间隔
        private float interval;
        private void AdjustPos()
        {
            if (handCards.Count == 0) return;
            for (int i = 0; i < handCards.Count; i++)
            {
                var minus = i - (handCards.Count-1)/2f;
                Vector2 direction = -(UIManager.instance.RotateCenter - handCards[i].originPos);
                Quaternion rotation = Quaternion.LookRotation(direction,Vector3.down);
                handCards[i].rectTransform.rotation = rotation;
                handCards[i].rectTransform.DORotate(rotation.eulerAngles,0.3f);
                handCards[i].ResetPosition(new Vector3((i-handCards.Count)*UIManager.instance.cardInterval+UIManager.instance.CenterCardPivot.position.x,UIManager.instance.CenterCardPivot.position.y));
            }
        }
    }
}
