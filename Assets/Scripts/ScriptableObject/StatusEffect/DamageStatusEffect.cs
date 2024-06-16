using UnityEngine;

[CreateAssetMenu( fileName = "DamageStatusEffect",menuName = "Status/Create DamageStatusEffect")]
public class DamageStatusEffect : StatusEffect
{
    [SerializeField] private float damagePerTick;

    public override void SetStatus(int targetId, StatusEffect statusEffect)
    {
        base.SetStatus(targetId, statusEffect);
        var damageStatusEffect = statusEffect as DamageStatusEffect;
        damagePerTick = damageStatusEffect.damagePerTick;
    }
    
    protected override void ActionTickEffect()
    {
        EventManager.TriggerOnSetDamageToEnemy(targetEnemyId, damagePerTick, WeaponDamageType.None);
    }
}