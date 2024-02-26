using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Manager;
using UnityEngine;

namespace Script.core
{
    public class Opponent : PlayerBase
    {
        public static Opponent instance;

        #region 三个值

        [SerializeField]
        private int _心动值 = 10;
        [SerializeField]
        private int _信任值 = 10;
        [SerializeField]
        private int _上头值 = 10;
        public override int 心动值
        {
            get => _心动值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("心动值太大");
                }
                else
                {
                    _心动值 = value;
                    UIManager.instance.opponentView.心动值.text = value.ToString();
                }
            }
        }
        public override int 信任值
        {
            get => _信任值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("信任值太大");
                }
                else
                {
                    _信任值 = value;
                    UIManager.instance.opponentView.信任值.text = value.ToString();
                }
            }
        }
        public override int 上头值
        {
            get => _上头值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("上头值太大");
                }
                else
                {
                    _上头值 = value;
                    UIManager.instance.opponentView.上头值.text = value.ToString();
                }
            }
        }

        #endregion
        
        public override void Awake()
        {
            base.Awake();
            instance = this;
        }

        private void Start()
        {
            心动值 = _心动值;
            信任值 = _信任值;
            上头值 = _上头值;
        }
    }
}