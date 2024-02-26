using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Script.Cards;
using Script.Character;
using Script.Manager;
using Script.view;
using UnityEngine;

namespace Script.core
{
    public abstract class PlayerBase : NetworkObject
    {
        public List<CardView> handCards;
        [HideInInspector]
        public CharacterBase character;
        [Header("角色名称,一定要是准确的全名")]
        public string characterName;
        #region 三个值
        
        public abstract int 心动值 { get; set; }
        public abstract int 信任值 { get; set; }
        public abstract int 上头值 { get; set; }

        #endregion

        public virtual void Awake()
        {
               
        }

        public virtual async UniTask PlayCard(Card card,List<NetworkObject> targets)
        {
             character.PlayCard(card, targets);
        }
        
        #region 抽卡

        public virtual async UniTask<Card> DrawCard()
        {
            return await character.DrawCard();
        }

        #endregion
    }
}