using System.Collections.Generic;
using System.Runtime.InteropServices;
using Script.core;

namespace Script.Cards
{
    /// <summary>
    /// 上头值+1, 心动值+1
    /// </summary>
    public class 送礼物 : Card
    {
        public 送礼物() : base()
        {
            
        }

        public override void Execute(List<NetworkObject> targets = null)
        {
            Player.instance.上头值++;
            Player.instance.心动值++;
        }
    }
}