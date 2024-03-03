using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using CareBoo.Serially;
using Script.core;
using Script.view;
using UnityEngine;
using UnityEngine.Serialization;
using Card = Script.Cards.Card;

namespace Script
{
    public class ObjectFactory : MonoBehaviour
    {
        public static ObjectFactory instance;
        public SerializedDictionary<string,NetworkObject> nameToObject = new SerializedDictionary<string, NetworkObject>();
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        public NetworkObject GetObject(string prefabName)
        {
            nameToObject.TryGetValue(prefabName, out var value);
            if (value != null)
            {
                return Instantiate(value);
            }
            return null;
        }
    }
}