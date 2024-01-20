using System;
using UnityEngine;

namespace Script.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance; 
        #region 卡牌相关
        [Header("卡牌中心位置锚点")]
        public Transform CenterCardPivot;
        [SerializeField]
        [Header("牌的间隔")]
        [Range(0,1)]
        private float m_cardInterval;
        /// <summary>
        /// 卡之间的间隔
        /// </summary>
        [HideInInspector] public float cardInterval;
        [Header("卡牌的父物体")]
        public Transform CardsParent;

        // [Header("施法区域")] public Image image;
        #endregion

        private void Awake()
        {
            instance = this;
            cardInterval = Screen.width * 0.1f;
            DontDestroyOnLoad(gameObject);
        }
    }
}