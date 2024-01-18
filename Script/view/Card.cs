using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Script.view
{
    [RequireComponent(typeof(InputHandler))]
    public class Card : MonoBehaviour,IMPointerEnterHandler,IMBeginDragHandler,IMDragHandler,IMEndDragHandler,IMPointerExitHandler
    {
        [HideInInspector]
        public RectTransform rectTransform;
        private RectTransform 施法区域;
        //原始的旋转
        [HideInInspector]
        public Vector3 rotate;
        //原始的RectTransform.AnchoredPosition
        [HideInInspector]
        public Vector2 originAnchoredPos;

        private Vector3 originScale;
        
        public core.Card card;

        public Sequence toBigModeTween;
        public Sequence backToOriginPosTween;
        /// <summary>
        /// 是否是可触发状态
        /// </summary>
        private bool _active = true;
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
        private readonly float enterUpDistance = 3;
        
        #endregion
        private void Awake()
        {
            施法区域 = GameObject.Find("施法区域").GetComponent<RectTransform>();
            rectTransform = GetComponent<RectTransform>();
            _shine = transform.Find("Shine");
            mainCamera = Camera.main;
            originAnchoredPos = rectTransform.anchoredPosition;
            originScale = transform.localScale;
            
            #region 定义四个MTweener
            
            toBigModeTween = DOTween.Sequence();
            backToOriginPosTween = DOTween.Sequence();
            
            
            toBigModeTween
                .Join(transform.DOScale(originScale*1.5f, 0.3f))
                .Join(DOTween.To(() => rectTransform.anchoredPosition,
                    value => rectTransform.anchoredPosition = value,
                    originAnchoredPos + new Vector2(0, enterUpDistance*transform.localScale.x), 0.2f))// 乘scale.x可以保持移动比例
                .Pause()
                .SetAutoKill(false);

            backToOriginPosTween
                .Join(transform.DOScale(originScale, 0.3f))
                .Join(DOTween.To(() => rectTransform.anchoredPosition,
                    value => rectTransform.anchoredPosition = value,
                    originAnchoredPos, 0.2f))
                .Pause()
                .SetAutoKill(false);

            #endregion
        }

        public void Init()
        {
            rectTransform = GetComponent<RectTransform>();
            _shine = transform.Find("Shine");
        }
        
        #region 一些事件函数
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!active) return;
            if (_bigMode) return;
            DOTween.Sequence()
                .Join(transform.DOScale(originScale * 1.5f, 0.3f))
                .Join(DOTween.To(() => rectTransform.anchoredPosition,
                    value => rectTransform.anchoredPosition = value,
                    originAnchoredPos + new Vector2(0, enterUpDistance), 0.2f));
            _bigMode = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!active) return;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!active) return;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!active) return;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if(!active)return;
            if (!_bigMode) return;
            _bigMode = false;
            DOTween.Sequence()
                .Join(transform.DOScale(originScale, 0.3f))
                .Join(DOTween.To(() => rectTransform.anchoredPosition,
                    value => rectTransform.anchoredPosition = value,
                    originAnchoredPos, 0.2f));
        }
        #endregion
    }
}
