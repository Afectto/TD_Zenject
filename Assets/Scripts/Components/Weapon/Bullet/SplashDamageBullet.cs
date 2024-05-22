using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageBullet : BulletBase
{
    [SerializeField] private float damageRadius;
    
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
}
