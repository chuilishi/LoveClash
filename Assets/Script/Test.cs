using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EasyButtons;
using Script.Cards;
using Script.core;
using Script.Network;
using Script.Utility;
using UnityEditor;
using UnityEngine;

namespace Script
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
            var json = JsonUtility.ToJson(new Operation());
        }
    }
}