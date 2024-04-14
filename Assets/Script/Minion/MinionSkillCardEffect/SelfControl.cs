using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CounterCardBase;

/// <summary>
/// 节制的技能卡：自我控制
/// </summary>
public class SelfControl : MinionSkillCardEffectBase, ICounter
{
    [SerializeField]
    private CounterType type = CounterType.AnyCard;

    public CounterType Type { get => type; set => type = value; }

    CounterType temp;

    protected override void Awake()
    {
        base.Awake();
        temp = Type;
    }

    public override void OnDisabled()
    {
        Type = CounterType.Null;
    }

    public override void OnEnabled()
    {
        Type = temp;

        //监听 PlayCardEvent
        EventManager.Register<PlayCardEvent>(Counter);

        //假设这张牌可能被反制，所以这里监听 CounterEvent，（实际上应该每次进行用卡时对 CounterEvent 进行监听，考虑写在 PlayCardEvent 内部）
        EventManager.Register<CounterEvent>(CounterEventTest);
    }

    public void Counter(PlayCardEvent playCardEvent)
    {
        //能够进行反制，则广播反制事件
        if (playCardEvent.counterType == type)
        {
            using var counterEvent = CounterEvent.Get();
            counterEvent.disable = true;
            EventManager.SendEvent(counterEvent);
        }
    }

    public void CounterEventTest(CounterEvent counterEvent)
    {
        disable = counterEvent.disable;
    }
}
