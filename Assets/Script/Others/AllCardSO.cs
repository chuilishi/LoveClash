using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Script.core;
using UnityEditor;
using UnityEngine;

namespace Script
{
    [CreateAssetMenu(menuName = "Card/卡牌库", fileName = "AllCards")]
    public class AllCardSO : ScriptableObject
    {
        public SerializedDictionary<string, NetworkObject> allCards;
    }
}