using System;
using System.Collections.Generic;
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
            Debug.Log("角色抽卡了");
            var values = Enum.GetValues(typeof(ObjectEnum));
            var card = await NetworkManager.InstantiateNetworkObject((ObjectEnum)new Random().Next(values.Length), UIManager.instance.CardsParent);
            UIManager.instance.playerView.DrawCard(card.GetComponent<CardView>());
            return card as Card;
        }
        #endregion
    }
}