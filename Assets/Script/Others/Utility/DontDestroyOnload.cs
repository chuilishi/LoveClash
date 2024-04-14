using System;
using UnityEngine;

namespace Script.Utility
{
    public class DontDestroyOnload: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}