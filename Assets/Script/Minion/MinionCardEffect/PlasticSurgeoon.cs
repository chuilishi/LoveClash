using Script.core;
using UnityEngine;

/// <summary>
/// ����ҽ��
/// </summary>
[RequireComponent(typeof(MinionCardEffect))]
public class PlasticSurgeoon : MinionCardEffectBase
{
    public override void OnDisabled()
    {
        if (!init) init = true;
        else return;
        Opponent.instance.��ͷֵ���� *= ((int)Opponent.instance.�Ա� + 1);
    }

    public override void OnEnabled()
    {
        if (init) init = false;
        else return;
        Opponent.instance.��ͷֵ���� /= ((int)Opponent.instance.�Ա� + 1);
    }
}
