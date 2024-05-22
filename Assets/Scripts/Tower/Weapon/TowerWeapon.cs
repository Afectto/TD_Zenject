using System.Collections;
using UnityEngine;
using Zenject;

public abstract class TowerWeapon : ShooterWeapon
{
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
        Utilities.DoForEachEnemyInRadius(transform.position, WeaponRange,enemyObject =>
        {
            SetTargetInstanceID(enemyObject.GetInstanceID(), enemyObject.transform);
        });
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
}
