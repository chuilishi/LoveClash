using System.Collections.Generic;
using Script.core;
using UnityEngine.Serialization;

namespace Script.Cards
{
    /// <summary>
    /// 基础卡
    /// </summary>
    public class BaseCardBase : Card
    {
        //使用这个牌能加上的值
        public int 增加的心动值;
        public int 增加的上头值;
        public int 增加的信任值;
        public override void Execute(PlayerBase playerBase, List<NetworkObject> targets = null)
        {
            playerBase.心动值 += 增加的心动值;
            playerBase.上头值 += 增加的上头值;
            playerBase.信任值 += 增加的信任值;
        }
    }
}