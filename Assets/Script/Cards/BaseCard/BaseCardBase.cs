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
        public override void Execute(Character character, List<NetworkObject> targets = null)
        {
            character.心动值 += 增加的心动值;
            character.上头值 += 增加的上头值;
            character.信任值 += 增加的信任值;
        }

        protected override void Awake()
        {
            base.Awake();
        }
    }
}