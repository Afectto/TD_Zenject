public class RicochetWeapon : TowerWeapon
{
     protected override IBullet CreateBullet()
     {
         var bullet = base.CreateBullet();
         bullet.onBulletDestroy += BulletDestroy;
         return bullet;
     }
     
     private void BulletDestroy(IBullet obj)
     {
         obj.OnSetDamage -= BulletOnSetDamage;
         obj.onBulletDestroy -= BulletDestroy;
     }
     
    protected override void BulletOnSetDamage(IBullet bullet, int targetInstanceID)
    {
        BulletSetDamage(targetInstanceID);
    }
}
