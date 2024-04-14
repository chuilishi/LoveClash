using Cysharp.Threading.Tasks;
using Script.Cards;
using Script.Character;
using Script.Manager;
using System;
using UnityEngine;

namespace Script.core
{
    public class Myself : PlayerBase
    {
        public static Myself instance;
        #region 三个值
        [SerializeField]
        private int _心动值;
        [SerializeField]
        private int _信任值;
        [SerializeField]
        private int _上头值;
        [SerializeField]
        private float _心动值倍率 = 1;
        [SerializeField]
        private float _信任值倍率 = 1;
        [SerializeField]
        private float _上头值倍率 = 1;
        [SerializeField]
        private Gender _性别 = Gender.Male;

        public override int 心动值
        {
            get => _心动值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("心动值太大");
                }
                else
                {
                    _心动值 = value;
                    UIManager.instance.myselfView.心动值.text = value.ToString();
                }
            }
        }
        public override int 信任值
        {
            get => _信任值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("信任值太大");
                }
                else
                {
                    _信任值 = value;
                    UIManager.instance.myselfView.信任值.text = value.ToString();
                }
            }
        }
        public override int 上头值
        {
            get => _上头值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("上头值太大");
                }
                else
                {
                    _上头值 = value;
                    UIManager.instance.myselfView.上头值.text = value.ToString();
                }
            }
        }

        public override float 心动值倍率
        {
            get => _心动值倍率;
            set
            {
                if (value > 99)
                {
                    throw new Exception("心动值倍率太大");
                }
                else
                {
                    _心动值倍率 = value;
                    UIManager.instance.myselfView.心动值.text = value.ToString();
                }
            }
        }

        public override float 信任值倍率
        {
            get => _信任值倍率;
            set
            {
                if (value > 99)
                {
                    throw new Exception("信任值倍率太大");
                }
                else
                {
                    _信任值倍率 = value;
                    UIManager.instance.myselfView.信任值.text = value.ToString();
                }
            }
        }

        public override float 上头值倍率
        {
            get => _上头值倍率;
            set
            {
                if (value > 99)
                {
                    throw new Exception("上头值倍率太大");
                }
                else
                {
                    _上头值倍率 = value;
                    UIManager.instance.myselfView.上头值.text = value.ToString();
                }
            }
        }

        public override Gender 性别
        {
            get => _性别;
            set
            {

                if (!Enum.IsDefined(typeof(Gender), value))
                {
                    throw new Exception("枚举值不是正常内容");
                }
                else
                {
                    _性别 = value;
                    
                    //UI 待完成
                    //UIManager.instance.playerView.性别.text = value.ToString();
                }
            }
        }

        #endregion
        public override void Awake()
        {
            base.Awake();
            instance = this;
            var characterType = Type.GetType(characterName);
            if (characterType == null)
            {
                var type = Type.GetType("Script.Character." + characterName);
                if(type == null) Debug.LogError("Character名称不正确");
                else
                {
                    var component = gameObject.AddComponent(type);
                    character = (CharacterBase)component;
                }
            }
        }

        private void Start()
        {
            心动值 = _心动值;
            信任值 = _信任值;
            上头值 = _上头值;
            心动值倍率 = _心动值倍率;
            信任值倍率 = _信任值倍率;
            上头值倍率 = _上头值倍率;
            性别 = _性别;
        }
        #region 抽卡
        /// <summary>
        /// 抽n张卡
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public override async UniTask<NetworkObject> DrawCard()
        {
            //这是牌库版本的老代码 
            // var card = Deck.instance.DrawCard();
            // if (card == null)
            // {
            //     Debug.Log("没牌了");
            //     return;
            // }
            // handCards.Add(card);
            // UIManager.instance.playerView.DrawCard(card);
            //Choose a Random Value from ObjectEnum
           return await character.DrawCard();
        }
        #endregion
    }
}
