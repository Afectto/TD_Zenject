public class EnemyShooterWeapon : ShooterWeapon
{
    protected override void BulletOnSetDamage(IBullet bullet, int targetInstanceID)
    {
        bullet.OnSetDamage -= BulletOnSetDamage;
        BulletSetDamage(targetInstanceID);
    }

}
