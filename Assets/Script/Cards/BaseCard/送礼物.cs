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

        public override void Execute(Character character,List<NetworkObject> targets = null)
        {
            character.上头值++;
            character.心动值++;
        }
    }
}