using Script.core;
using UnityEngine;

/// <summary>
/// 整形医生
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class PlasticSurgeoon : MinionCardEffectBase
{
    public override void OnDisabled()
    {
        if (!init) init = true;
        else return;
        Opponent.instance.上头值倍率 *= ((int)Opponent.instance.性别 + 1);
    }

    public override void OnEnabled()
    {
        if (init) init = false;
        else return;
        Opponent.instance.上头值倍率 /= ((int)Opponent.instance.性别 + 1);
    }
}
