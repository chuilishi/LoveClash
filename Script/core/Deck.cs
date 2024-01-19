using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Script.core
{
    public class Deck
    {
        public List<Card> curCards = new List<Card>();
        private view.Deck deckView;

        public void Init(view.Deck deckView)
        {
            UniTask.WaitUntil((() => deckView != null&&deckView.curCards!=null));
            curCards = deckView.curCards.Select((card => card.card)).ToList();
            this.deckView = deckView;
        }
        //抽卡的时候是抽倒数第一张卡
        public Card DrawCard()
        {
            if(curCards.Count==0)return null;
            var card = curCards[^1];
            curCards.RemoveAt(curCards.Count-1);
            return card;
        }
        /// <summary>
        /// 抽第几张
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Card DrawCard(int index)
        {
            if (index >= curCards.Count || index < 0) return null;
            var card = curCards[index];
            curCards.RemoveAt(index);
            return card;
        }
    }
}