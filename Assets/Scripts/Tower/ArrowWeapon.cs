public class ArrowWeapon : TowerWeapon
{
    protected override void BulletOnSetDamage(IBullet bullet, int targetInstanceID)
    {
        BulletSetDamage(targetInstanceID);
    }

}
