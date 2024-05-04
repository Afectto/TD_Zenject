using System.Collections;
using UnityEditor;
using UnityEngine;

public class ShooterWeapon : Weapon
{
    [Space(10)]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform firePoint;

    protected override IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackRite);
            CreateBullet();
        }
        // ReSharper disable once IteratorNeverReturns
    }
    
    private void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.speed = bulletSpeed;
        bullet.OnSetDamage += BulletOnSetDamage;
        bullet.OnSetTarget(TargetTransform);
    }

    private void BulletOnSetDamage(Bullet obj)
    {
        obj.OnSetDamage -= BulletOnSetDamage;
        EventManager.TriggerOnSetDamage(TargetInstanceID, damage);
    }
}