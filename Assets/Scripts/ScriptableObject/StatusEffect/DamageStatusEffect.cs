using Unity.Collections;
using UnityEngine;

[CreateAssetMenu( fileName = "DamageStatusEffect",menuName = "Status/Create DamageStatusEffect")]
public class DamageStatusEffect : StatusEffect
{
    [Header("Damage Status Component")]
    [SerializeField] private float damagePerTick;  
    
    [HideInInspector]public string totalDamage;  

    private void OnValidate()
    {
        if (tickInterval > 0)
        {
            totalDamage = ((int)(duration / tickInterval) * damagePerTick).ToString();
        }
        else
        {
            totalDamage = "NULL" + "\ntickInterval = 0" + "\nFIX IT!!!!!";
            
        }
    }
    
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