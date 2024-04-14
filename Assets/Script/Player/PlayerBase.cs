using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Character;
using Script.Minion;
using Script.view;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.core
{
    public abstract class PlayerBase : NetworkObject
    {
        [HideInInspector]
        public CharacterBase character;
        [Header("角色名称,一定要是准确的全名")]
        public string characterName;
        #region 三个值

        public abstract int 心动值 { get; set; }
        public abstract int 信任值 { get; set; }
        public abstract int 上头值 { get; set; }

        #endregion

        #region 三个倍率、性别
        public abstract float 心动值倍率 { get; set; }
        public abstract float 信任值倍率 { get; set; }
        public abstract float 上头值倍率 { get; set; }
        public abstract Gender 性别 { get; set; }
        #endregion

        //键值考虑用 string、卡牌基类
        //期望效果：get<Minion>(id) 可得到卡牌 card 对象
        
        //泛型解释：
        //第一层：卡牌基类 List
        //第二层：id -> 卡牌基类 List
        //第三层：卡牌具体类型 -> id -> 卡牌基类 List
        public Dictionary<Type, Dictionary<int, List<CardBase>>> allCards;
        public void AddCards<T>(int id, T card) where T : CardBase
        {
            Type type = typeof(T);
            if (!allCards.ContainsKey(type))
            {
                allCards.Add(type, new Dictionary<int, List<CardBase>>());
            }
            if (!allCards[type].ContainsKey(id))
            {
                allCards[type].Add(id, new List<CardBase>());
            }
            allCards[type][id].Add(card);
        }

        /// <summary>
        /// 玩家可能有重复牌，所以返回值为 List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> GetCard<T>(int id) where T : CardBase
        {
            Type type = typeof(T);
            if (allCards.ContainsKey(type))
            {
                if (allCards[type].ContainsKey(id))
                {
                    //强转为 T 类型的 list
                    return allCards[type][id].Cast<T>().ToList();
                }
            }
            return null;
        }

        public List<ICounter> GetCounterCard()
        {
            List<ICounter> counters = new List<ICounter>();
            foreach (var card in allCards)
            {
                if (card.Key is ICounter)
                {
                    counters.Add((ICounter)card.Key);
                }
            }
            return counters;
        }
        

        /// <summary>
        /// 性别枚举
        /// </summary>
        public enum Gender
        {
            Male,
            Female
        }

        public virtual void Awake()
        {
            allCards = new Dictionary<Type, Dictionary<int, List<CardBase>>>();
        }

        public virtual void PlayCard(int card, List<int> targets)
        {
            character.PlayCard(card, targets);
        }

        #region 抽卡

        public virtual async UniTask<NetworkObject> DrawCard()
        {
            return await character.DrawCard();
        }

        #endregion
    }
}