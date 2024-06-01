using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Zenject;
using IJob = Unity.Jobs.IJob;

public abstract class TowerWeapon : ShooterWeapon
{
    [Inject] private Armor _armor;
    [Inject] private TowerWeaponMultiplayer _weaponMultiplayer;
    
    private float _baseDamage;
    private float _baseAttackRite;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Attack());
        _baseDamage = damage;
        _baseAttackRite = attackRite;
        
        AddDamage(weaponDamageType);
        AddAttackRite(weaponDamageType);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _weaponMultiplayer.OnAddDamageBuff += AddDamage;
        _weaponMultiplayer.OnAddSpeedBuff += AddAttackRite;
    }

    private void AddDamage(WeaponDamageType damageType)
    {
        if (damageType == weaponDamageType)
        {
            damage = _baseDamage * _weaponMultiplayer.GetDamageMultiplayer(damageType);
        }
    }

    private void AddAttackRite(WeaponDamageType damageType)
    {
        if (damageType == weaponDamageType)
        {
            attackRite = _baseAttackRite / _weaponMultiplayer.GetAttackRiteMultiplayer(damageType);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _weaponMultiplayer.OnAddDamageBuff += AddDamage;
        _weaponMultiplayer.OnAddSpeedBuff += AddAttackRite;
    }
    
    private void FindTarget()
    {
        // var enemyList = Utils.GetAllTagObjectInRadius("Enemy", transform.position, WeaponRange);
        // NativeArray<Vector3> enemyPositions = new NativeArray<Vector3>(enemyList.Count, Allocator.TempJob);
        // NativeArray<Vector3> targetPosition = new NativeArray<Vector3>(1, Allocator.TempJob);
        //
        // for (int i = 0; i < enemyList.Count; i++)
        // {
        //     enemyPositions[i] = enemyList[i].transform.position;
        // }
        //
        // FindTargetJob job = new FindTargetJob
        // {
        //     enemyPositions = enemyPositions,
        //     targetPosition = targetPosition,
        // };
        //
        // JobHandle jobHandle = job.Schedule();
        // jobHandle.Complete();
        //
        // Vector3 finalTargetPosition = targetPosition[0];
        //
        // enemyPositions.Dispose();
        // targetPosition.Dispose();
        //
        // foreach (var enemy in enemyList)
        // {
        //     if (enemy.transform.position == finalTargetPosition)
        //     {
        //         SetTargetInstanceID(enemy.GetInstanceID(), enemy.transform);
        //         return;
        //     }
        // }
        GameObject closestEnemy = null;
        float highestDamage = 0.0f;
        float closestDistance = float.MaxValue;
        
        Utils.ForEachEnemyInRadius(transform.position, WeaponRange,enemyObject =>
        {
            var armor = enemyObject.GetComponent<ArmorBehaviour>();
            var damageToEnemy = _armor.CalculateDamage(damage, weaponDamageType, armor.ArmorType, armor.Armor);
     
            float distanceToEnemy = Vector3.Distance(transform.position, enemyObject.transform.position);
    
            if (damageToEnemy > highestDamage || (damageToEnemy == highestDamage && distanceToEnemy < closestDistance))
            {
                closestEnemy = enemyObject;
                highestDamage = damageToEnemy;
                closestDistance = distanceToEnemy;
            }
        });

        if (closestEnemy)
        {
            SetTargetInstanceID(closestEnemy.GetInstanceID(), closestEnemy.transform);
        }
    }

    protected override IEnumerator Attack()
    {
        while (true)
        {
            if (TargetTransform)
            {
                CreateBullet();
                yield return new WaitForSeconds(attackRite);
            }
            else
            {
                FindTarget();
            }

            yield return null;
        }
        // ReSharper disable once IteratorNeverReturns
    }

    protected override void BulletSetDamage(int targetInstanceID)
    {
        EventManager.TriggerOnSetDamageToEnemy(targetInstanceID, damage, weaponDamageType);
    }
}

[BurstCompatible]
public struct FindTargetJob : IJob
{
    [ReadOnly]
    public NativeArray<Vector3> enemyPositions;
    public NativeArray<Vector3> targetPosition;
    
    public void Execute()
    {
        Vector3 nearestEnemyPos = Vector3.zero;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < enemyPositions.Length; i++)
        {
            float distance = Vector3.Distance(enemyPositions[i], targetPosition[0]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemyPos = enemyPositions[i];
            }
        }

        targetPosition[0] = nearestEnemyPos;
    }
}
