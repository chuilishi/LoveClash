using Script.Manager;
using TMPro;
using UnityEngine;

namespace Script.view
{
    public class OpponentView : PlayerView
    {
        public override void Awake()
        {
            base.Awake();
            UIManager.instance.opponentView = this;
        }
    }
}