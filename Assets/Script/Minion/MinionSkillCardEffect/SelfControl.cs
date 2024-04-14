using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CounterCardBase;

/// <summary>
/// ���Ƶļ��ܿ������ҿ���
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

        //���� PlayCardEvent
        EventManager.Register<PlayCardEvent>(Counter);

        //���������ƿ��ܱ����ƣ������������ CounterEvent����ʵ����Ӧ��ÿ�ν����ÿ�ʱ�� CounterEvent ���м���������д�� PlayCardEvent �ڲ���
        EventManager.Register<CounterEvent>(CounterEventTest);
    }

    public void Counter(PlayCardEvent playCardEvent)
    {
        //�ܹ����з��ƣ���㲥�����¼�
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
