using System.Collections;
using UnityEngine;

public abstract class ShooterWeapon : Weapon
{
    [Space(5)]
    [Header("Shooter Component")]
    [SerializeField] private BulletBase bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform firePoint;
    

    protected override IEnumerator Attack()
    {
        while (true)
        {
            CreateBullet();
            yield return new WaitForSeconds(attackRite);
        }
        // ReSharper disable once IteratorNeverReturns
    }
    
    protected virtual void CreateBullet()
    {
        if (!TargetInfo.IsEmpty())
        {
            CreateBullet(TargetInfo);
        }

    }

    protected IBullet CreateBullet(TargetInfo targetInfo)
    {
        var difference = targetInfo.TargetTransform.position - transform.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        var rotation  = Quaternion.Euler(0f, 0f, rotationZ);
        
        var bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        bullet.OnSetDamage += BulletOnSetDamage;
        bullet.OnBulletDestroy += BulletDestroy;
        bullet.OnSetSpeedAndTarget(bulletSpeed, targetInfo.TargetTransform);

        return bullet;
    }
    
    private void BulletDestroy(IBullet obj)
    {
        obj.OnSetDamage -= BulletOnSetDamage;
        obj.OnBulletDestroy -= BulletDestroy;
    }
    
    private void BulletOnSetDamage(IBullet bullet, int targetInstanceID)
    {
        BulletSetDamage(targetInstanceID);
    }

    protected virtual void BulletSetDamage(int targetInstanceID)
    {
        EventManager.TriggerOnSetDamage(targetInstanceID, damage);
    }

    public void SetFirePoint(Transform point)
    {
        firePoint = point;
    }
}