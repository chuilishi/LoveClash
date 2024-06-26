using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MinionCardEffect : EffectBase
{
    public override void Trigger(List<int> targetIds = null)
    {
        if (onTrigger != null)
            onTrigger();
    }

    public event Action onTrigger;
}
