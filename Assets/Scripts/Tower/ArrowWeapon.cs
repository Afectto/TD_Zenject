public class ArrowWeapon : TowerWeapon
{
    protected override void BulletOnSetDamage(IBullet bullet, int targetInstanceID)
    {
        bullet.OnSetDamage -= BulletOnSetDamage;
        BulletSetDamage(targetInstanceID);
    }

}
