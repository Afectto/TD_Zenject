using System.Collections;
using UnityEngine;

public class ShooterWeapon : Weapon
{
    [Space(15)]
    [SerializeField] private Bullet bulletPrefab;
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
    
    protected void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.OnSetDamage += BulletOnSetDamage;
        bullet.OnSetSpeedAndTarget(bulletSpeed, TargetTransform);
    }

    private void BulletOnSetDamage(Bullet obj)
    {
        obj.OnSetDamage -= BulletOnSetDamage;
        EventManager.TriggerOnSetDamage(TargetInstanceID, damage);
    }
}