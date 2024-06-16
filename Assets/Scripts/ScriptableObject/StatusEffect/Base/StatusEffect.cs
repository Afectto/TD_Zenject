using System;
using UnityEngine;
using Zenject;

public enum StatusEffectType
{
    Fire,
    Poison,
    Ice
}

public interface IStatusEffect
{
    void SetStatus(int targetId, StatusEffect statusEffect);
    void UpdateStatus();
    int GetTargetId();
    void RestartStatusEffect();
}

[Serializable]
public abstract class StatusEffect : ScriptableObject, IStatusEffect, IListener
{
    [Inject] public EventManager EventManager { get; }
    [SerializeField] protected StatusEffectType statusEffectStatus;
    [SerializeField] protected float duration;
    [SerializeField] protected float tickInterval;
    [SerializeField] protected bool isStackable;
    protected int targetEnemyId;
    protected int tickCount;
    protected float lastUpdateTime;

    public StatusEffectType StatusEffectStatus => statusEffectStatus;
    public float Duration => duration;
    public float TickInterval => tickInterval;
    public bool IsStackable => isStackable;
    public bool IsActive => tickCount > 0;

    public virtual void SetStatus(int targetId, StatusEffect statusEffect)
    {
        statusEffectStatus = statusEffect.StatusEffectStatus;
        duration = statusEffect.Duration;
        tickInterval = statusEffect.TickInterval;
        
        targetEnemyId = targetId;
        tickCount = (int)(duration / tickInterval);
        lastUpdateTime = Time.time;
    }

    public virtual void RemoveStatus(int targetId)
    {
        if (targetId == targetEnemyId)
        {
            EventManager.TriggerOnRemoveActiveStatus(this);
        }
    }

    public virtual void UpdateStatus()
    {
        if(!IsActive) return;
        
        if (Time.time - lastUpdateTime > tickInterval)
        {
            ActionTickEffect();
            tickCount--;
            lastUpdateTime = Time.time;
        }

        if (tickCount <= 0)
        {
            RemoveStatus(targetEnemyId);
        }
    }

    public int GetTargetId()
    {
        return targetEnemyId;
    }

    public void RestartStatusEffect()
    {
        tickCount = (int)(duration / tickInterval) - 1;
    }

    public void OnEnable()
    {
        if (EventManager != null)
        {
            EventManager.OnDeath += RemoveStatus;
        }
    }

    public void OnDisable()
    {
        if (EventManager != null)
        {
            EventManager.OnDeath -= RemoveStatus;
        }
    }
    
    protected abstract void ActionTickEffect();
}