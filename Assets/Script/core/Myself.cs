using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Script.Cards;
using Script.Character;
using Script.Manager;
using Script.Network;
using Script.view;
using UnityEngine;
using Random = System.Random;

namespace Script.core
{
    public class Myself : PlayerBase
    {
        public static Myself instance;
        #region 三个值
        [SerializeField]
        private int _心动值;
        [SerializeField]
        private int _信任值;
        [SerializeField]
        private int _上头值;
        
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
                    UIManager.instance.playerView.心动值.text = value.ToString();
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
                    UIManager.instance.playerView.信任值.text = value.ToString();
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
                    UIManager.instance.playerView.上头值.text = value.ToString();
                }
            }
        }

        #endregion
        public override void Awake()
        {
            base.Awake();
            instance = this;
            var characterType = Type.GetType(characterName);
            if (characterType == null)
            {
                var type = Type.GetType("Script.Character." + characterName);
                if(type == null) Debug.LogError("Character名称不正确");
                else
                {
                    var component = gameObject.AddComponent(type);
                    character = (CharacterBase)component;
                }
                
            }
        }

        private void Start()
        {
            心动值 = _心动值;
            信任值 = _信任值;
            上头值 = _上头值;
        }
        #region 抽卡
        /// <summary>
        /// 抽n张卡
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public override async UniTask<Card> DrawCard()
        {
            //这是牌库版本的老代码 
            // var card = Deck.instance.DrawCard();
            // if (card == null)
            // {
            //     Debug.Log("没牌了");
            //     return;
            // }
            // handCards.Add(card);
            // UIManager.instance.playerView.DrawCard(card);
            //Choose a Random Value from ObjectEnum
           return await character.DrawCard();
        }
        #endregion
    }
}
