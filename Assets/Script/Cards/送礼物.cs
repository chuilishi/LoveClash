using System.Runtime.InteropServices;
using Script.core;

namespace Script.Cards
{
    /// <summary>
    /// 上头值+1, 心动值+1
    /// </summary>
    [Guid("e62c90b1-4aa8-49b4-a0a9-249270b25650")]
    public class 送礼物 : Card
    {
        public 送礼物() : base()
        {
            
        }

        public override void Execute(Card target = null)
        {
            Player.instance.上头值++;
            Player.instance.心动值++;
        }
    }
}