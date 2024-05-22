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
        
        var difference = TargetTransform.position - transform.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        var rotation  = Quaternion.Euler(0f, 0f, rotationZ);
        
        var bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        bullet.OnSetDamage += BulletOnSetDamage;
        bullet.onBulletDestroy += BulletDestroy;
        bullet.OnSetSpeedAndTarget(bulletSpeed, TargetTransform);

        return bullet;
    }
    
    private void BulletDestroy(IBullet obj)
    {
        obj.OnSetDamage -= BulletOnSetDamage;
        obj.onBulletDestroy -= BulletDestroy;
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