using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/New CardSO", order = 1)]
    public class CardSO : ScriptableObject
    {
        public Sprite sprite;
        [FormerlySerializedAs("card")] public CardBase cardBase;
    }
}