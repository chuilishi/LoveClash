using System;
using Script.core;
using Script.Network;
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

        [Header("随从中心位置锚点")] public Transform centerMinionPivot;
        [Header("卡牌中心位置锚点")] public Transform centerCardPivot;

        [SerializeField] [Header("牌的间隔")] [Range(0, 1)]
        private float m_cardInterval;

        /// <summary>
        /// 卡之间的间隔
        /// </summary>
        [HideInInspector] public float cardInterval;

        [HideInInspector] public float minionInterval = 39;

        [Header("卡牌的父物体")] public Transform CardsParent;
        [Header("施法区域")] public Image 施法区域;
        [Header("通知版")] public GameObject 通知板;
        [Header("物品池")] public GameObject 物品池;
        [Header("弃牌堆")] public GameObject 弃牌堆;
        [Header("回合结束")] public Button 回合结束;
        [Header("卡牌回手の区域")] public RectView 卡牌回手区域;
        #endregion

        #region 一些核心引用
        public MyselfView myselfView;
        public OpponentView opponentView;
        #endregion

        #region 一些变量
        /// <summary>
        /// 卡的移动速度
        /// </summary>
        [Header("卡的速度")] public float cardSpeed = 1;

        #endregion

        private void Awake()
        {
            instance = this;
            cardInterval = Screen.width * 0.1f;
            通知板.gameObject.SetActive(false);
            DontDestroyOnLoad(gameObject);
            回合结束.onClick.AddListener(() =>
            {
                if (GameProcessor.CurPlayer == NetworkManager.instance.playerEnum)
                {
                    Operation operation = new Operation(OperationType.EndTurn);
                    OperationExecutor.instance.Execute(operation);
                }
            });
        }
    }
}