using UnityEngine;

[CreateAssetMenu(fileName = "IceStatusEffect",menuName = "Status/Create IceStatusEffect")]
public class IceStatusEffect : NonDamageStatusEffect
{
    [SerializeField] private float speedRedaction;

    public override void SetStatus(int targetId, StatusEffect statusEffect)
    {
        base.SetStatus(targetId, statusEffect);
        statusEffectStatus = StatusEffectType.Ice;
        var iceStatusEffect = statusEffect as IceStatusEffect;
        speedRedaction = iceStatusEffect.speedRedaction;
    }

    protected override void TriggerActiveStatus()
    {
        EventManager.TriggerOnChangeIceStatus(targetEnemyId, -speedRedaction);
    }

    protected override void TriggerRemoveStatus()
    {
        EventManager.TriggerOnChangeIceStatus(targetEnemyId, speedRedaction);
    }
}