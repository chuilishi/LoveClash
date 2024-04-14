using Script.Cards;
using Script.core;
using Script.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Script.Minion
{
    /// <summary>
    /// 随从卡
    /// </summary>
    public abstract class MinionCardBase : CardBase
    {
        public abstract int MinionTableId { get; set; }

        public abstract MinionTableItem MinionTableItem { get; set; }

        /// <summary>
        /// 卡牌功能类型枚举
        /// </summary>
        public enum FunctionType
        {
            /// <summary>
            /// 自身效果
            /// </summary>
            Myself,
            /// <summary>
            /// 对方效果
            /// </summary>
            Opponent,
            /// <summary>
            /// 全局效果
            /// </summary>
            Whole,
            /// <summary>
            /// 随从类型
            /// </summary>
            Minion,
            /// <summary>
            /// 卡牌类型
            /// </summary>
            Card
        }

        public override void Execute(PlayerBase playerBase, List<int> targetIds = null)
        {
            onExtraExecute(playerBase);
        }
        public virtual void onExtraExecute(PlayerBase playerBase)
        {

        }

        protected MinionTableItem GetMinionTableItemById(int id)
        {
            return TableManager.instance.GetMinionTable().GetItemById(id);
        }

        protected void GetMinionTableItemById()
        {
            MinionTableItem = TableManager.instance.GetMinionTable().GetItemById(MinionTableId);
        }

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            GetMinionTableItemById();
            transform.Find("NameIcon").GetComponentInChildren<TextMeshProUGUI>().text = MinionTableItem.minionName;
            transform.Find("Description").GetComponent<TextMeshProUGUI>().text = MinionTableItem.decription;
        }
    }
}
