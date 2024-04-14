using Script.core;
using Script.Manager;
using Script.view;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinionCardEffect))]
public abstract class MinionCardEffectBase : NetworkObject
{
    private MinionCardEffect minionCardEffect;

    protected int round;
    protected int remaindRound;

    /// <summary>
    /// ��ֹ�ظ���Ч�Ĳ������漰�����籶�ʡ�����ʱ������
    /// </summary>
    protected bool init;

    /// <summary>
    /// ���غ��Ƿ��Ѿ��ù������ƺ���̫��Ҫ
    /// </summary>
    protected bool used;

    /// <summary>
    /// �Ƿ��ڽ���״̬
    /// </summary>
    public bool disable;

    protected virtual void Awake()
    {
        minionCardEffect = GetComponent<MinionCardEffect>();
        round = GetComponent<MinionCard>().MinionTableItem.round;
        remaindRound = GetComponent<MinionCard>().MinionTableItem.remaindRound;
        if (GetComponent<MinionCard>().MinionTableItem.round != 0)
        {
            minionCardEffect.onTrigger += RoundJudge;
            minionCardEffect.onTrigger += OnTriggerEffect;
        }
        else
        {
            //�غ���Ϊ 0��Ҳ��������ȴʱ�俨�ƣ���ֱ�����ã����ﲻʹ���¼�ע����Է�ֹ���������������Ľ������紥��һ�μ�ע��
            OnTriggerEffect();
        }
        EventManager.Register<TurnOverEvent>(RoundCount);
    }

    /// <summary>
    /// �غ�������
    /// </summary>
    protected void RoundCount(TurnOverEvent turnOverEvent)
    {
        remaindRound = Mathf.Max(0, --remaindRound);
        used = false;
    }

    /// <summary>
    /// ��ȡ��Ӽ��ܿ�
    /// </summary>
    protected void DrawMinionSkillCard()
    {
        int id = GetComponent<MinionCard>().MinionTableItem.minionSkillCardId;
        if (id != 0)
        {
            UIManager.instance.myselfView.DrawCard<MinionSkillCard>(GetComponent<ICardView>(), id);
        }
    }

    /// <summary>
    /// �غ����ж���ʣ��غ���Ϊ 0 �� disable Ϊ false
    /// </summary>
    protected void RoundJudge()
    {
        if (remaindRound <= 0)
        {
            remaindRound = round;
            disable = false;
        }
        else
        {
            disable = true;
        }
    }

    protected virtual void OnTriggerEffect()
    {
        if (!used) used = true;
        else return;
        if (!disable) { OnEnabled(); DrawMinionSkillCard(); }
        else { OnDisabled(); }
    }

    /// <summary>
    /// ����ʱ���ã�����ĸ d �� Unity �����������ֿ���
    /// </summary>
    public abstract void OnEnabled();

    /// <summary>
    /// ������ʱ���ã�������ħ�ļ��ܿɽ������
    /// </summary>
    public abstract void OnDisabled();

    private void OnDisable()
    {
        minionCardEffect.onTrigger -= OnTriggerEffect;
        minionCardEffect.onTrigger -= RoundJudge;
        EventManager.Unregister<TurnOverEvent>(RoundCount);
    }
}
