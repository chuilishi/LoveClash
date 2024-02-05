using System;

namespace Script.Cards.BaseCard
{
    /// <summary>
    /// 上头值+1
    /// </summary>
    [Serializable]
    public class 喷香水 : BaseCardBase
    {
        //在编辑器内也能改
        //TODO 最后应该是要从表格内读取,暂时应付一下
        protected override void Awake()
        {
            base.Awake();
            增加的上头值 = 1;
        }
    }
}