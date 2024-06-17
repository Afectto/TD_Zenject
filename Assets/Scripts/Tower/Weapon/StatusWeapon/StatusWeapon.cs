using UnityEngine;

public abstract class StatusWeapon : TowerWeapon
{
    [Space(5)]
    [Header("Status Component")]
    [SerializeField] protected StatusEffectType _statusEffectType;
    
    protected override void BulletSetDamage(int targetInstanceID)
    {
        base.BulletSetDamage(targetInstanceID);
        SetStatusEffect(targetInstanceID);
    }
    
    protected virtual void SetStatusEffect(int targetInstanceID)
    {
        EventManager.TriggerOnSetStatus(_statusEffectType, targetInstanceID);
    }
}