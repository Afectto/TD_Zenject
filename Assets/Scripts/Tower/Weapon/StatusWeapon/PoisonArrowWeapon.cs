public class PoisonArrowWeapon : ArrowWeapon
{
    protected override void BulletSetDamage(int targetInstanceID)
    {
        base.BulletSetDamage(targetInstanceID);
        EventManager.TriggerOnSetStatus(StatusEffectType.Poison, targetInstanceID);
    }
}
