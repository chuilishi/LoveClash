using System.Collections.Generic;
using Script.core;
using Script.Network;
using UnityEngine;

namespace Script.Effects.其他Effect
{
    public class DrawCardEffect : EffectBase
    {
        public override void Trigger(List<int> targetIds)
        {
            _ = Myself.instance.DrawCard();

            var drawCardEvent = DrawCardEvent.Get();
            drawCardEvent.playerEnum = NetworkManager.instance.playerEnum;
            EventManager.SendEvent(drawCardEvent);
        }
    }
}