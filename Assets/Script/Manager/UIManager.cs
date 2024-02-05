using System;
using Script.view;
using TMPro;
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
        [Header("通知版")] public GameObject 通知板;
        [Header("物品池")] public GameObject 物品池;
        #endregion

        #region 一些核心引用
        
        [FormerlySerializedAs("player")] public PlayerView playerView;
        [FormerlySerializedAs("opponent")] public OpponentView opponentView;
        #endregion
        
        private void Awake()
        {
            instance = this;
            cardInterval = Screen.width * 0.1f;
            通知板.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
    }
}