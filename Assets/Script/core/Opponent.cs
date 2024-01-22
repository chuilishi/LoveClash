using System;
using Script.Manager;
using UnityEngine;

namespace Script.core
{
    public class Opponent : MonoBehaviour
    {
        public static Opponent instance;
        [SerializeField]
        private int _心动值 = 10;
        [SerializeField]
        private int _信任值 = 10;
        [SerializeField]
        private int _上头值 = 10;
        public int 心动值
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
                    UIManager.instance.opponent.心动值.text = value.ToString();
                }
            }
        }
        public int 信任值
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
                    UIManager.instance.opponent.信任值.text = value.ToString();
                }
            }
        }
        public int 上头值
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
                    UIManager.instance.opponent.上头值.text = value.ToString();
                }
            }
        }
    }
}