using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using Script.core;
using Script.Manager;
using Script.Network;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.view
{
    [RequireComponent(typeof(InputHandler))]
    public class MinionView : ICardView
    {
        /// <summary>
        /// 7个随从的rect,中间的是 minionRects[3]
        /// </summary>
        private List<Rect> minionRects = new List<Rect>();
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
        //所有随从的初始位置
        private List<Vector3> originPoses = new List<Vector3>();
        //用来放一个空Minion的
        private static MinionView emptyMinionView = null;
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
        private void Start()
        {
            if (emptyMinionView == null)
            {
                emptyMinionView = new GameObject().AddComponent<MinionView>();
                emptyMinionView.enabled = false;
            }
            emptyMinionView.transform.SetParent(UIManager.instance.物品池.transform);
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
                .Join(transform.DOMove(originPos,Vector2.Distance(transform.position,originPos)/UIManager.instance.cardSpeed))
                .Join(transform.DOScale(Vector3.one,Mathf.Abs(transform.localScale.x-1)/UIManager.instance.cardSpeed))
                .Play().ToUniTask();
            active = true;
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (!active) return;
            dragOffset = (Vector2)transform.position - eventData.position;
        }
        bool inHands = true;
        private int curIndex = -1;
        public override void OnDrag(PointerEventData eventData)
        {
            if (!active) return;
            transform.position = eventData.position + dragOffset;
            if (UIManager.instance.卡牌回手区域.rect.Contains(transform.position))
            {
                //非inHands到inHands
                if (inHands == false)
                {
                    UIManager.instance.myselfView.minions.Remove(emptyMinionView);
                    UIManager.instance.myselfView.AdjustMinionsPos();
                }
                inHands = true;
            }
            else 
            {
                // 从inHands到非inHands
                if (inHands)
                {
                    inHands = false;
                    var index = originPoses.BinarySearch(transform.position,Comparer<Vector3>.Create(((a, b) => a.x.CompareTo(b.x))));
                    if (index < 0)
                    {
                        index = ~index;
                    }
                    if (index != curIndex)
                    {
                        UIManager.instance.myselfView.minions.Remove(emptyMinionView);
                        UIManager.instance.myselfView.minions.Insert(index,emptyMinionView);
                        UIManager.instance.myselfView.AdjustMinionsPos();
                        originPoses.Clear();
                        foreach (var minion in UIManager.instance.myselfView.minions)
                        {
                            originPoses.Add(minion.transform.position);
                        }
                        curIndex = index;
                    }
                }
            }
        }
        public void PlayCard()
        {
            var index = UIManager.instance.myselfView.minions.IndexOf(emptyMinionView);
            UIManager.instance.myselfView.minions[index] = this;
            UIManager.instance.myselfView.AdjustMinionsPos();
            Execute(new Operation(OperationType.Card,networkId));
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (!active) return;
            if (!UIManager.instance.卡牌回手区域.rect.Contains(eventData.position))
            {
                PlayCard();
            }
            else
            {
                UIManager.instance.myselfView.minions.Remove(emptyMinionView);
                UIManager.instance.myselfView.AdjustMinionsPos();
                _ = ResetPosition();
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