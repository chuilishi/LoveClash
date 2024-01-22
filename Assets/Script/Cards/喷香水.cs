using System;
using System.Runtime.InteropServices;
using Script.core;

namespace Script.Cards
{
    /// <summary>
    /// 上头值+1
    /// </summary>
    [Serializable]
    [Guid("2A711325-154C-C67F-432E-66500D0453C3")]
    public class 喷香水 : Card
    {
        public 喷香水(view.Card cardView) : base(cardView){}
        public override void Execute(Card target = null)
        {
            Player.instance.上头值++;
        }
    }
}