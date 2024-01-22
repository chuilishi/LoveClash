using System;
using Script.core;
using UnityEngine;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void GameStart()
        {
            
        }
    }
}