using UnityEngine;

[CreateAssetMenu(fileName = "IceStatusEffect",menuName = "Status/Create IceStatusEffect")]
public class IceStatusEffect : NonDamageStatusEffect
{
    [SerializeField, Range(0.001f, 1)] private float percentSpeedRedaction;

    public override void SetStatus(int targetId, StatusEffect statusEffect)
    {
        base.SetStatus(targetId, statusEffect);
        statusEffectStatus = StatusEffectType.Ice;
        var iceStatusEffect = statusEffect as IceStatusEffect;
        percentSpeedRedaction = iceStatusEffect.percentSpeedRedaction;
    }

    protected override void TriggerActiveStatus()
    {
        EventManager.TriggerOnChangeIceStatus(targetEnemyId, percentSpeedRedaction);
    }

    protected override void TriggerRemoveStatus()
    {
        EventManager.TriggerOnChangeIceStatus(targetEnemyId, 1);
    }
}