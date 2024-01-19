using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance; 
        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #region 卡牌相关

        public Transform CenterCardPivot;
        public float cardInterval = Screen.width * 0.01f;
        //卡牌有略微旋转,手牌在手中是在一个圆弧上,此为圆心
        public Vector3 RotateCenter =  UIManager.instance.CenterCardPivot.position - new Vector3(0, Screen.height*0.01f,0);

        #endregion

    }
}