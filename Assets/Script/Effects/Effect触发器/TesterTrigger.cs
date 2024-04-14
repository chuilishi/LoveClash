using EasyButtons;
using UnityEditor.Compilation;
using UnityEngine;

namespace Script.Effects.Effect触发器
{
    /// <summary>
    /// 用来测试的
    /// </summary>
    public class TesterTrigger : EffectTrigger
    {
        [Button]
        public void Trigger()
        {
            TriggerEffects(null);
        }
    }
}