using System;
using System.Collections.Generic;
using DG.Tweening;
using EasyButtons;
using TMPro;
using UnityEngine;

namespace Script.view
{
    public class Player : MonoBehaviour
    {
        public static Player instance;
        public core.Player player;
        public List<Card> handCards = new List<Card>();
        public TMP_Text 心动值;
        public TMP_Text 上头值;
        public TMP_Text 信任值;
        [Header("卡牌的出生位置")]
        public Transform cardInitPos;
        private List<Vector3> handCardPoses = new List<Vector3>();
        private Transform cardsParent;

        private void Awake()
        {
            instance = this;
            cardsParent = transform.Find("Cards");
            心动值 = transform.Find("心动值/心动值Text").gameObject.GetComponent<TextMeshPro>();
            上头值 = transform.Find("上头值/上头值Text").gameObject.GetComponent<TextMeshPro>();
            信任值 = transform.Find("信任值/信任值Text").gameObject.GetComponent<TextMeshPro>();
        }

        private void Start()
        {
            centerOfCircle = midPos - new Vector3(0, 100f, 0);
        }
        [Button]
        public void DrawCard(int num)
        {
            for (int i = 0; i < num; i++)
            {
                var card = player.DrawCard();
                if(card==null)break;
                handCards.Add(card.cardView);
            }
            AdjustPos();
        }
        //anchoredPosition为单位的间隔
        private float interval = 50f;
        [Header("手牌中心位置")]
        public Vector3 midPos;
        //卡牌有略微旋转,手牌在手中是在一个圆弧上,此为圆心
        private Vector3 centerOfCircle;
        private void AdjustPos()
        {
            if (handCards.Count == 0) return;
            for (int i = 0; i < handCards.Count; i++)
            {
                var minus = i - (handCards.Count-1)/2f;
                var i1 = i;
                
                DOTween.To(()=>handCards[i1].rectTransform.anchoredPosition,(value) => handCards[i1].rectTransform.anchoredPosition = value,
                    new Vector2(handCards[i1].rectTransform.anchoredPosition.y,(i1-handCards.Count)*interval),0.3f);
                Vector2 direction = -(centerOfCircle - handCards[i1].rectTransform.position);
                Quaternion rotation = Quaternion.LookRotation(direction,Vector3.down);
                handCards[i].rectTransform.rotation = rotation;
                handCards[i].rectTransform.DORotate(rotation.eulerAngles,0.3f);
                handCards[i].originAnchoredPos = handCards[i].rectTransform.anchoredPosition;
                handCards[i].backToOriginPosTween.Restart();
            }
        }
    }
}
