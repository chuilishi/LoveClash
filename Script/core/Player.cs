using System.Collections.Generic;
using System.Linq;

namespace Script.core
{
    public class Player
    {
        public static Player instance;
        public Deck deck;
        public List<Card> handCards;
        public view.Player playerView;
        public int 心动值;
        public int 信任值;
        public int 上头值;

        public void Init(view.Player playerView,Deck deck,List<Card> handCards,(int,int,int) 心动信任上头值)
        {
            instance = this;
            this.playerView = playerView;
            this.deck = deck;
            this.handCards = handCards;
            心动值 = 心动信任上头值.Item1;
            信任值 = 心动信任上头值.Item2;
            上头值 = 心动信任上头值.Item3;
        }
        /// <summary>
        /// 抽n张卡
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public List<Card> DrawCard(int num = 1)
        {
            List<Card> cards = new ();
            for (int i = 0; i < num; i++)
            {
               cards.Add( deck.DrawCard());
            }
            playerView.DrawCard(cards);
            return cards;
        }
        /// <summary>
        /// 抽指定Index
        /// </summary>
        /// <param name="num"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public List<Card> DrawCard(List<int>indexes)
        {
            var list = new List<Card>();
            indexes.Sort();
            for (int i = indexes.Count-1; i > 0; i--)
            {
                list.Add(deck.DrawCard(i));
            }
            list.Reverse();
            return list;
        }
    }
}
