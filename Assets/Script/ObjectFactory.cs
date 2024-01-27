using System;
using System.Collections.Generic;
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
        public GenericDictionary<string,NetworkObject> nameToObject = new GenericDictionary<string, NetworkObject>();
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        public NetworkObject GetObject(string name)
        {
            try
            {
                return nameToObject[name];
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}