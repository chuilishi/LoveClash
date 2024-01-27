using System;
using System.Collections.Generic;
using System.Net.Sockets;
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
        private TcpClient client = new TcpClient();
        private async void Awake()
        {
            await client.ConnectAsync("43.136.95.76", 7777);
        }

        private void Update()
        {
            UnityEngine.Debug.Log("asdf");
        }
    }
}