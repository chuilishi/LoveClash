using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EasyButtons;
using Script.Cards;
using Script.core;
using Script.Utility;
using UnityEditor;
using UnityEngine;

namespace Script
{
    public class Test : MonoBehaviour
    {
        public Card cardView;
        private void Awake()
        {
            Debug.Log(cardView.GetType());
        }
    }
}