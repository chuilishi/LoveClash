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
    /// <summary>
    /// 随机抽Card(非随从) 用来测试
    /// </summary>
    public class RandomCardCharacter : CharacterBase
    {
        public override void PlayCard(int card, List<int> targets)
        {
            //TODO 单机
            card.ToNetworkObject().GetComponent<EffectTrigger>().TriggerEffects(targets);
        }
        public override async UniTask<NetworkObject> DrawCard()
        {
            var randomCardName = ObjectFactory.Instance.allCardsName[new Random().Next(0, ObjectFactory.Instance.allCardsName.Count)];
            var card = await NetworkManager.InstantiateNetworkObject(randomCardName, UIManager.instance.CardsParent);
            UIManager.instance.myselfView.DrawCard(card.GetComponent<ICardView>());
            return card;
        }
    }
}