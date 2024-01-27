using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Script.Cards;
using Script.Manager;
using Script.Network;
using UnityEngine;

namespace Script.core
{
    public class Player : MonoBehaviour
    {
        public static Player instance;
        public List<Cards.Card> handCards;
        public PlayerEnum playerEnum;
        #region 三个值

        [SerializeField]
        private int _心动值;
        [SerializeField]
        private int _信任值;
        [SerializeField]
        private int _上头值;
        
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
                    UIManager.instance.player.心动值.text = value.ToString();
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
                    UIManager.instance.player.信任值.text = value.ToString();
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
                    UIManager.instance.player.上头值.text = value.ToString();
                }
            }
        }

        #endregion
        
        public void GameStart()
        {
            DrawCard();
        }

        #region 打牌

        public async UniTask PlayCard(Card card,List<NetworkObject> targets)
        {
            
        }

        #endregion
        
        #region 抽卡

        /// <summary>
        /// 抽n张卡
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        [Button]
        public void DrawCard()
        {
            var card = Deck.instance.DrawCard();
            if (card == null)
            {
                Debug.Log("没牌了");
                return;
            }
            handCards.Add(card);
            UIManager.instance.player.DrawCard(card);
        }
        /// <summary>
        /// 抽指定Index, 0代表数组中最后一张牌(即实际牌堆顶的第一张)
        /// </summary>
        /// <param name="num"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public List<Cards.Card> DrawCard(List<int>indexes)
        {
            return Deck.instance.DrawCard(indexes);
        }

        #endregion
        
    }
}
