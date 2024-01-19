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
        public Vector3 originPos;

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
        private readonly float enterUpDistance = 200;
        
        #endregion
        private void Awake()
        {
            施法区域 = GameObject.Find("施法区域").GetComponent<RectTransform>();
            rectTransform = GetComponent<RectTransform>();
            _shine = transform.Find("Shine");
            mainCamera = Camera.main;
            rectTransform = GetComponent<RectTransform>();
            _shine = transform.Find("Shine");
        }

        private void Start()
        {
            originPos = transform.position;
        }

        public void Init()
        {
        }
        
        #region 一些事件函数
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!active) return;
            if (_bigMode) return;
            DOTween.Sequence()
                .Join(transform.DOScale(Vector3.one*1.5f, 0.3f))
                .Join(DOTween.To(() => transform.position,
                    value => transform.position = value,
                    originPos + new Vector3(0, Screen.height*0.125f,0), 0.2f));
            _bigMode = true;
        }
        //设置并回到原位置
        public void ResetPosition(Vector3? position = null)
        {
            if (position != null)originPos = position.GetValueOrDefault();
            DOTween.Sequence()
                .Join(transform.DOScale(Vector3.one,0.3f))
                .Join(DOTween.To(() => transform.position,
                    value => transform.position = value,
                    originPos, 0.2f)).Play();
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
            ResetPosition();
        }
        #endregion
    }
}
