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
        
        damage = _baseDamage * _weaponMultiplayer.GetDamageMultiplayer(weaponDamageType);
        attackRite = _baseAttackRite * _weaponMultiplayer.GetAttackRiteMultiplayer(weaponDamageType);
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, WeaponRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                SetTargetInstanceID(collider.gameObject.GetInstanceID(), collider.transform);
            }
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
}
