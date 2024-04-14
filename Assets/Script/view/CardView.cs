using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script.Cards;
using Script.core;
using Script.Manager;
using Script.Network;
using Script.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Script.view
{
    [RequireComponent(typeof(InputHandler))]
    public class CardView : ICardView
    {
        //原始的RectTransform.AnchoredPosition
        [HideInInspector]
        public Vector3 originPos;
        /// <summary>
        /// 是否是可触发状态
        /// </summary>
        private bool _active = true;
        /// <summary>
        /// 开始拖动时的偏移量
        /// </summary>
        private Vector2 dragOffset;
        public bool active
        {
            get => _active;
            set => _shine.gameObject.SetActive(value);
        }
        // 发亮组件
        private Transform _shine;
        //是否是放大状态
        private bool _bigMode = false;
        private Camera mainCamera;
        
        #region 一些动画属性
        //放大时向上移动的距离
        // private readonly float enterUpDistance = 200;
        
        #endregion
        private void Awake()
        {
            _shine = transform.Find("Shine");
            mainCamera = Camera.main;
            _shine = transform.Find("Shine");
            originPos = transform.position;
        }
        public async void PlayCard(PlayerView playerView)
        {
            Execute(new Operation(OperationType.Card,networkId));
            transform.SetParent(UIManager.instance.弃牌堆.transform);
            active = false;
            playerView.handCards.Remove(this);
            await ResetPosition(UIManager.instance.弃牌堆.transform.position);
        }
        #region 一些事件函数
        public override async void OnPointerEnter(PointerEventData eventData)
        {
            if (!active) return;
            if (_bigMode) return;
            active = false;
            _bigMode = true;
            await DOTween.Sequence()
                .Join(transform.DOScale(Vector3.one * 1.5f, 0.3f))
                .Join(DOTween.To(() => transform.position,
                    value => transform.position = value,
                    originPos + new Vector3(0, Screen.height * 0.125f, 0), 0.2f)).Play().ToUniTask();
            active = true;
        }
        //设置并回到原位置(不传入就是回到原位置)
        public override async UniTask ResetPosition(Vector3? position = null)
        {
            if (position != null)originPos = position.GetValueOrDefault();
            active = false;
            await DOTween.Sequence()
                .Join(transform.DOMove(originPos,0.2f))
                .Join(transform.DOScale(Vector3.one, 0.3f))
                .Play().ToUniTask();
            active = true;
        }
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (!active) return;
            dragOffset = (Vector2)transform.position - eventData.position;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (!active) return;
            transform.position = eventData.position + dragOffset;
        }

        public override async void OnEndDrag(PointerEventData eventData)
        {
            if (!active) return;
            if (!UIManager.instance.卡牌回手区域.rect.Contains(eventData.position))
            {
                PlayCard(UIManager.instance.myselfView);
            }
            else
            {
                await ResetPosition();
            }
        }
        public override async void OnPointerExit(PointerEventData eventData)
        {
            if(!active)return;
            if (!_bigMode)return;
            _bigMode = false;
            active = false;
            await ResetPosition();
            active = true;
        }
        #endregion
    }
}
