public class FireArrowWeapon : ArrowWeapon
{
    protected override void BulletSetDamage(int targetInstanceID)
    {
        base.BulletSetDamage(targetInstanceID);
        EventManager.TriggerOnSetStatus(StatusEffectType.Fire, targetInstanceID);
    }
}
