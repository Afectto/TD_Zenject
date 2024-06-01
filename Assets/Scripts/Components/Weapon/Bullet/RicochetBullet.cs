using UnityEngine;

public class RicochetBullet : BulletBase
{
    [SerializeField]private int maxRicochetCount;
    [SerializeField] private float chainRadius;
    private int _ricochetLeft;

    private void Awake()
    {
        _ricochetLeft = maxRicochetCount;
    }

    protected override void SetDamage()
    {
        InvokeSetDamage(Target.gameObject.GetInstanceID());
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
        Enemy targetEnemy = Target.GetComponentInParent<Enemy>();
        int targetEnemyID = targetEnemy.GetInstanceID();

        Enemy foundEnemy = null;

        Utils.ForEachEnemyInRadius(center, radius,(enemyObject) =>
        {
            Enemy enemy = enemyObject.GetComponentInParent<Enemy>();
            if (enemy.GetInstanceID() != targetEnemyID && foundEnemy == null)
            {
                foundEnemy = enemy;
            }
        });

        return foundEnemy;
    }
    
}
