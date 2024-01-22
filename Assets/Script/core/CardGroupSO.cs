using System.Collections.Generic;
using UnityEngine;

namespace Script.core
{
    [CreateAssetMenu(fileName = "New Data", menuName = "Card/New CardGroupSO", order = 1)]
    public class CardGroupSO : ScriptableObject
    {
        public List<view.CardView> cards;
    }
}