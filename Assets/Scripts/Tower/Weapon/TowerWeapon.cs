using System.Collections;
using UnityEngine;

public abstract class TowerWeapon : ShooterWeapon
{
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Attack());
    }

    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnAddDamageBuffToTowerWeapon += AddDamage;
        EventManager.OnAddSpeedBuffToTowerWeapon += AddAttackRite;
    }

    private void AddDamage(float value, WeaponDamageType damageType)
    {
        if (damageType == weaponDamageType)
        {
            damage += damage * value / 100;
        }
    }

    private void AddAttackRite(float value, WeaponDamageType damageType)
    {
        if (damageType == weaponDamageType)
        {
            attackRite -= attackRite * value / 100;
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnAddDamageBuffToTowerWeapon += AddDamage;
        EventManager.OnAddSpeedBuffToTowerWeapon += AddAttackRite;
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
