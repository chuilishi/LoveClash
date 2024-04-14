using System;
using System.Collections.Generic;
using Script.core;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Cards
{
    [Serializable]
    public abstract class CardBase : NetworkObject
    {
        public virtual void Execute(core.PlayerBase player, List<int> targetIds = null)
        {
            GetComponent<EffectTrigger>().TriggerEffects(targetIds);
        }

        private void OnDestroy()
        {
            EventManager.UnregisterTarget(this);
        }
    }
}