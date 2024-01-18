using System.Collections.Generic;

namespace Script.core
{
    public class Player
    {
        public Deck deck;
        public List<Card> handCards;
        public view.Player playerView;
        public int 心动值;
        public int 信任值;
        public int 上头值;

        public void Init(view.Player playerView)
        {
            this.playerView = playerView;
        }
        
        public List<Card> DrawCard(int num)
        {
            var list = new List<Card>();
            for (int i = 0; i < num; i++)
            {
                list.Add(deck.DrawCard());
            }
            return list;
        }
    }
}