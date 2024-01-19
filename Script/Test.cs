using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EasyButtons;
using Script.core;
using UnityEngine;

namespace Script
{
    public class Test : MonoBehaviour
    {
        public float t = 0;

        [Button]
        public void DrawCard()
        {
            Player.instance.DrawCard();
        }
    }
}