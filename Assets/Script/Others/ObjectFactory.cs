using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using EasyButtons;
using Script.core;
using Script.view;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class ObjectFactory : MonoBehaviour
    {
        public static ObjectFactory Instance;
        [SerializeField] private AllCardSO cardNames;
        [SerializeField] private AllCardSO minionNames;
        public SerializedDictionary<string,NetworkObject> cardNameToObject = new SerializedDictionary<string, NetworkObject>();
        public SerializedDictionary<string, NetworkObject> minionNameToObject =
            new SerializedDictionary<string, NetworkObject>();
        [HideInInspector]
        public List<string> allCardsName = new List<string>();
        [HideInInspector]
        public List<string> allMinionsName = new List<string>();
        [Button("保存修改")]
        public void WriteIntoSO()
        {
            cardNames.allCards = cardNameToObject;
            minionNames.allCards = minionNameToObject;
        }
        [Button("读取")]
        public void ReadFromSO()
        {
            cardNameToObject = cardNames.allCards;
            minionNameToObject = minionNames.allCards;
        }
        private void Awake()
        {
            Instance = this;
            cardNameToObject = cardNames.allCards;
            minionNameToObject = minionNames.allCards;
            allCardsName = cardNameToObject.Select(pair => pair.Key).ToList();
            allMinionsName = minionNameToObject.Select(pair => pair.Key).ToList();
        }
        public NetworkObject GetObject(string prefabName)
        {
            NetworkObject value;
            cardNameToObject.TryGetValue(prefabName, out value);
            if (value != null)
            {
                return value;
            }

            minionNameToObject.TryGetValue(prefabName, out value);
            if (value != null)
            {
                return value;
            }
            return null;
        }
    }
}