using System.Collections.Generic;
using System.Linq;
namespace Script.core
{
    public class Player
    {
        public static Player instance;
        public Deck deck;
        public List<Cards.Card> handCards;
        public view.Player playerView;
        public int 心动值;
        public int 信任值;
        public int 上头值;

        public void Init(view.Player playerView,Deck deck,(int,int,int) 心动信任上头值)
        {
            instance = this;
            this.playerView = playerView;
            this.deck = deck;
            心动值 = 心动信任上头值.Item1;
            信任值 = 心动信任上头值.Item2;
            上头值 = 心动信任上头值.Item3;
        }
        /// <summary>
        /// 抽n张卡
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<Cards.Card> DrawCard(int num = 1)
        {
            List<Cards.Card> cards = new ();
            for (int i = 0; i < num; i++)
            {
                var card = deck.DrawCard();
                if(card==null)break;
                cards.Add(card);
            }
            playerView.DrawCard(cards);
            return cards;
        }
        /// <summary>
        /// 抽指定Index, 0代表数组中最后一张牌(即实际牌堆顶的第一张)
        /// </summary>
        /// <param name="num"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public List<Cards.Card> DrawCard(List<int>indexes)
        {
            return deck.DrawCard(indexes);
        }
    }
}
