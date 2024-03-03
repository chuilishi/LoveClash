using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.core;
using Script.Manager;
using Script.Network;
using Script.view;
using UnityEngine;
using Random = System.Random;

namespace Script.Character
{
    public abstract class CharacterBase : MonoBehaviour
    {
        public abstract void PlayCard(Card card, List<NetworkObject> targets);
        #region 抽卡

        //异步是因为暂时是
        public virtual async UniTask<Card> DrawCard()
        {
            //选择<string,Object>字典中的某个随机key
            var randomPrefabName = ObjectFactory.instance.nameToObject.Keys.ToArray()[new Random().Next(0, ObjectFactory.instance.nameToObject.Count)];
            var card = await NetworkManager.InstantiateNetworkObject(randomPrefabName, UIManager.instance.CardsParent);
            UIManager.instance.playerView.DrawCard(card.GetComponent<CardView>());
            return card as Card;
        }
        #endregion
    }
}