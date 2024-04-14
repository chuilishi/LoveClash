using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.core;
using Script.Manager;
using Script.Network;
using Script.view;
using UnityEngine;

namespace Script.Character
{
    public class HaoKan : CharacterBase
    {
        public override void PlayCard(int card, List<int> targets)
        {
            if (card.ToNetworkObject().GetComponent<BaseCardEffect>() != null)
            {
                Myself.instance.心动值++;
            }
            card.ToNetworkObject().GetComponent<EffectTrigger>().TriggerEffects(targets);
        }
        /// <summary>
        /// 暂时是随机抽卡(方便测试)
        /// </summary>
        /// <returns></returns>
        public override async UniTask<NetworkObject> DrawCard()
        {
            var randomCardName = ObjectFactory.Instance.allCardsName[Random.Range(0, ObjectFactory.Instance.allCardsName.Count)];
            var card = await NetworkManager.InstantiateNetworkObject(randomCardName, UIManager.instance.CardsParent);
            UIManager.instance.myselfView.DrawCard(card.GetComponent<ICardView>());
            return card;
        }
    }
}