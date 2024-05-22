using System.Collections;
using UnityEngine;

public abstract class ShooterWeapon : Weapon
{
    [Space(15)]
    [SerializeField] private BulletBase bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform firePoint;
    
    protected abstract void BulletOnSetDamage(IBullet bullet, int targetInstanceID);
    
    protected override IEnumerator Attack()
    {
        while (true)
        {
            CreateBullet();
            yield return new WaitForSeconds(attackRite);
        }
        // ReSharper disable once IteratorNeverReturns
    }
    
    protected virtual IBullet CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.OnSetDamage += BulletOnSetDamage;
        bullet.OnSetSpeedAndTarget(bulletSpeed, TargetTransform);

        return bullet;
    }

    protected void BulletSetDamage(int targetInstanceID)
    {
        EventManager.TriggerOnSetDamage(targetInstanceID, damage);
    }

    public void SetFirePoint(Transform point)
    {
        firePoint = point;
    }
}