using Script.Manager;
using TMPro;
using UnityEngine;

namespace Script.view
{
    public class OpponentView : CharacterView
    {
        public override void Awake()
        {
            base.Awake();
            UIManager.instance.opponentView = this;
        }
    }
}