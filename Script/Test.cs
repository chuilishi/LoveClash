using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Script
{
    public class Test : MonoBehaviour
    {
        public float t = 0;
        private async void Start()
        {
            // var tweener = transform.DOMove(transform.position + new Vector3(1, 0, 0), 1f).SetAutoKill(false);
            // await UniTask.Delay(2000);
            // tweener.Restart();
        }
    }
}