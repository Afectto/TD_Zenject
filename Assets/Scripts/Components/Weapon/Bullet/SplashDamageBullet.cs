using System;
using UnityEngine;

public class SplashDamageBullet : BulletBase
{
    [SerializeField] private float damageRadius;

    public void OnEnable()
    {
        onEndMoveBulletToDeadTarget += SetDamage;
    }
    
    protected override void SetDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                InvokeSetDamage(collider.gameObject.GetInstanceID());
            }
        }

        DestroyBullet();
    }
    
    public void OnDisable()
    {
        onEndMoveBulletToDeadTarget -= SetDamage;
    }
}
