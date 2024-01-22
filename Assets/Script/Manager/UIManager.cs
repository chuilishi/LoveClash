using System;
using Script.view;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        #region 卡牌相关

        [Header("卡牌中心位置锚点")] public Transform centerCardPivot;

        [SerializeField] [Header("牌的间隔")] [Range(0, 1)]
        private float m_cardInterval;

        /// <summary>
        /// 卡之间的间隔
        /// </summary>
        [HideInInspector] public float cardInterval;

        [Header("卡牌的父物体")] public Transform CardsParent;
        [Header("施法区域")] public Image battleField;
        [Header("正在连接中")] public Image 正在连接中;
        [FormerlySerializedAs("cardView")] [Header("通用卡面")] public CardView cardViewView;
        #endregion

        #region 一些核心引用
        
        public Player player;
        public Opponent opponent;
        #endregion
        
        private void Awake()
        {
            instance = this;
            cardInterval = Screen.width * 0.1f;
            正在连接中.enabled = false;
            DontDestroyOnLoad(gameObject);
        }
    }
}