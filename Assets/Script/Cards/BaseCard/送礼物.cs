using System.Collections.Generic;
using System.Runtime.InteropServices;
using Script.core;

namespace Script.Cards.BaseCard
{
    /// <summary>
    /// 上头值+1, 心动值+1
    /// </summary>
    public class 送礼物 : BaseCardBase
    {
        protected override void Awake()
        {
            base.Awake();
            增加的上头值 = 1;
            增加的心动值 = 1;
        }
    }
}