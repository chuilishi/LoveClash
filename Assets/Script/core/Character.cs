using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EasyButtons;
using Script.Cards;
using Script.Manager;
using Script.view;
using UnityEngine;

namespace Script.core
{
    public abstract class Character : NetworkObject
    {
        public List<CardView> handCards;
        #region 三个值
        
        public abstract int 心动值 { get; set; }
        public abstract int 信任值 { get; set; }
        public abstract int 上头值 { get; set; }

        #endregion
        #region 打牌

        public abstract UniTask PlayCard(Card card, List<NetworkObject> targets);
        #endregion
        #region 抽卡
        
        public abstract void DrawCard();
        public abstract List<Card> DrawCard(List<int> indexes);

        #endregion
    }
}