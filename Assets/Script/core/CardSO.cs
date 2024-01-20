using UnityEngine;

namespace Script.Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/New CardSO", order = 1)]
    public class CardSO : ScriptableObject
    {
        public Sprite sprite;
        public Card card;
    }
}