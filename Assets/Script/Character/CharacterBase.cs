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
        public abstract void PlayCard(int card, List<int> targets);
        #region 抽卡
        //异步是因为暂时是
        public abstract UniTask<NetworkObject> DrawCard();

        #endregion
    }
}