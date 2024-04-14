using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EasyButtons;
using Script.Cards;
using Script.core;
using Script.Manager;
using Script.Minion;
using TMPro;
using UnityEngine;

namespace Script.view
{
    public class MyselfView : PlayerView
    {
        public override void Awake()
        {
            base.Awake();
            UIManager.instance.myselfView = this;
        }
        /// <summary>
        /// 抽Cards.Card入手牌
        /// </summary>
        /// <param name="cards"></param>
        public UniTask DrawCard(List<Cards.CardBase>cards)
        {
            if (cards.Count == 0)
            {
                Debug.Log("没牌了");
                return UniTask.CompletedTask;
            }
            return AdjustHandCardsPos();
        }
        public void DrawCard(ICardView cardView)
        {
            try
            {
                handCards.Add(cardView);

                AdjustHandCardsPos();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 获取指定卡牌，需要卡牌 TableId
        /// 或者：改进 TableManager 的查询方法，提供泛型查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cardView"></param>
        /// <param name="id"></param>
        public void DrawCard<T>(ICardView cardView, int id) where T : CardBase
        {
            try
            {
                handCards.Add(cardView);
                //这里是通过 CardView 再去获取卡牌基类脚本，如果不想要 CardView 参数，参数里就换成 T
                T cardBase = cardView.GetComponent<T>();
                if (cardBase != null)
                {
                    Myself.instance.AddCards(id, cardBase);
                }

                AdjustHandCardsPos();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private T CreateInstance<T>(Type type) where T : class
        {
            //if (!typeof(T).IsAssignableFrom(type))
            if (type != null)
            {
                return (T)Activator.CreateInstance(type);
            }
            else
            {
                return null;
            }
        }

        //anchoredPosition为单位的间隔
        private float interval;
        private UniTask AdjustHandCardsPos()
        {
            if (handCards.Count == 0) return UniTask.CompletedTask;
            UniTask[] tasks = new UniTask[handCards.Count];
            for (int i = 0; i < handCards.Count; i++)
            {
                var minus = i - (handCards.Count-1)/2f;
                var position = UIManager.instance.centerCardPivot.position;
                tasks[i] = handCards[i].ResetPosition(new Vector3(minus*UIManager.instance.cardInterval+position.x,position.y));
            }
            return UniTask.WhenAll(tasks);
        }
        public UniTask AdjustMinionsPos()
        {
            if (minions.Count == 0) return UniTask.CompletedTask;
            UniTask[] tasks = new UniTask[minions.Count];
            for (int i = 0; i < minions.Count; i++)
            {
                var minus = i - (minions.Count-1)/2f;
                var position = UIManager.instance.centerMinionPivot.position;
                if(minions[i] == null)continue;
                tasks[i] = minions[i].ResetPosition(new Vector3(minus*UIManager.instance.minionInterval+position.x,position.y));
            }
            return UniTask.WhenAll(tasks);
        }
    }
}
