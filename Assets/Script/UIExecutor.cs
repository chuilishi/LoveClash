using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Script
{
    /// <summary>
    /// 提供了一个主UI播放队列
    /// </summary>
    public class UIExecutor : MonoBehaviour
    {
        public static UIExecutor instance;
        private static Queue<Func<UniTask>> uiQueue = new Queue<Func<UniTask>>();
        private static int uiCount = 0;
        private void Awake()
        {
            instance = this;
            Main();
        }
        public async UniTask Main()
        {
            while (true)
            {
                await UniTask.WaitUntil((() => uiQueue.Count!=0));
                var action = uiQueue.Dequeue();
                await action.Invoke();
                uiCount++;
            }
        }

        public static async UniTask ExecuteAsync(Func<UniTask> uiAction)
        {
            uiQueue.Enqueue(uiAction);
            int resUICount = uiQueue.Count;
            var curCount = uiCount;
            await UniTask.WaitUntil((() => uiCount==curCount+resUICount));
        }

        public static void Execute(Func<UniTask> uiAction)
        {
            uiQueue.Enqueue(uiAction);
        }
    }
}