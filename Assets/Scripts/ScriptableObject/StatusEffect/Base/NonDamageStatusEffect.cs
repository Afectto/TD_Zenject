public abstract class NonDamageStatusEffect : StatusEffect
{
    private int _maxTickCount;

    public override void SetStatus(int targetId, StatusEffect statusEffect)
    {
        base.SetStatus(targetId, statusEffect);
        _maxTickCount = tickCount;
    }

    protected override void ActionTickEffect()
    {
        if (tickCount == _maxTickCount)
        {
            TriggerActiveStatus();
        }
    }
    
    public override void RemoveStatus(int targetId)
    {
        base.RemoveStatus(targetId);
        TriggerRemoveStatus();
    }

    protected abstract void TriggerActiveStatus();
    protected abstract void TriggerRemoveStatus();
}

