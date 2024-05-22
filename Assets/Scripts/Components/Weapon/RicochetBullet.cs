using System;
using UnityEngine;

public class RicochetBullet : BulletBase
{
    [SerializeField]private int maxRicochetCount;
    [SerializeField] private float chainRadius;
    private int _ricochetLeft;

    public override event Action<IBullet, int> OnSetDamage;

    private void Awake()
    {
        _ricochetLeft = maxRicochetCount;
    }

    protected override void SetDamage()
    {
        OnSetDamage?.Invoke(this, Target.gameObject.GetInstanceID());
        if (_ricochetLeft > 0)
        {
            _ricochetLeft--;
            FindNewTarget();
            return;
        }
        DestroyBullet();
    }

    private void FindNewTarget()
    {
        var enemy = FindEnemyInRadius(gameObject.transform.position, chainRadius);
        if (enemy && enemy.gameObject.activeSelf )
        {
            Target = enemy.transform;
        }
    }
    
    private Enemy FindEnemyInRadius(Vector3 center, float radius)
    {
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(center, radius);
        var targetEnemy = Target.GetComponentInParent<Enemy>();
        var targetEnemyID = targetEnemy.GetInstanceID();
        
        for (int i = 0; i < allColliders.Length; i++)
        {
            if (allColliders[i].CompareTag("Enemy"))
            {
                var enemy = allColliders[i].GetComponentInParent<Enemy>();
                if (enemy.GetInstanceID() != targetEnemyID)
                {
                    return enemy;
                }
            }
        }

        return null;
    }
}
