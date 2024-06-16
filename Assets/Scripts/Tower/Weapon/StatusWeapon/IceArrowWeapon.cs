public class IceArrowWeapon : ArrowWeapon
{
    protected override void BulletSetDamage(int targetInstanceID)
    {
        base.BulletSetDamage(targetInstanceID);
        EventManager.TriggerOnSetStatus(StatusEffectType.Ice, targetInstanceID);
    }
}
