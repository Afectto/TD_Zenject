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
        Utilities.ForEachEnemyInRadius(transform.position, damageRadius,(enemyObject) =>
        {
            InvokeSetDamage(enemyObject.GetInstanceID());
        });

        DestroyBullet();
    }
    
    public void OnDisable()
    {
        onEndMoveBulletToDeadTarget -= SetDamage;
    }
}
