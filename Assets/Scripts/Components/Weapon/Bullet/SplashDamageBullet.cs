using UnityEngine;

public class SplashDamageBullet : BulletBase
{
    [SerializeField] private float damageRadius;

    public void OnEnable()
    {
        OnEndMoveBulletToDeadTarget += SetDamage;
    }
    
    protected override void SetDamage()
    {
        Utils.ForEachEnemyInRadius(transform.position, damageRadius,(enemyObject) =>
        {
            InvokeSetDamage(enemyObject.GetInstanceID());
        });

        DestroyBullet();
    }
    
    public void OnDisable()
    {
        OnEndMoveBulletToDeadTarget -= SetDamage;
    }
}
