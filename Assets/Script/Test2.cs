using System;
using UnityEngine;

namespace Script
{
    public class Test2 : MonoBehaviour
    {
        public Test test;
        private void Start()
        {
            Instantiate(test);
            Debug.Log("After Instantiate");
        }
    }
}