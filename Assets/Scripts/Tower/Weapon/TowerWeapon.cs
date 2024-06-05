using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public abstract class TowerWeapon : ShooterWeapon
{
    [Inject] protected Armor _armor;
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
        _weaponMultiplayer.OnAddDamageBuff += AddDamage;
        _weaponMultiplayer.OnAddSpeedBuff += AddAttackRite;
    }
    
    protected virtual void FindTarget()
    {
        GameObject enemy = null;
        float highestDamage = 0.0f;
        float closestDistance = float.MaxValue;
        
        Utils.ForEachEnemyInRadius(transform.position, WeaponRange,enemyObject =>
        {
            var armorBehaviour = enemyObject.GetComponent<ArmorBehaviour>();
            
            var damageToEnemy = _armor.CalculateDamage(damage, weaponDamageType, armorBehaviour.ArmorType, armorBehaviour.Armor);
            float distanceToEnemy = Vector3.Distance(transform.position, enemyObject.transform.position);
            
            bool variantFindEnemy = GetVariantFindEnemy(distanceToEnemy, closestDistance, damageToEnemy, highestDamage);
            
            if (variantFindEnemy)
            {
                enemy = enemyObject;
                highestDamage = damageToEnemy;
                closestDistance = distanceToEnemy;
            }
        });

        if (enemy)
        {
            SetTargetInstanceID(new TargetInfo(enemy.GetInstanceID(), enemy.transform));
        }
    }

    protected bool GetVariantFindEnemy(float distanceToEnemy, float closestDistance, 
                                     float damageToEnemy, float highestDamage)
    {
        var randomValue = Random.Range(0, 10000);
        var variantFindEnemy = distanceToEnemy < closestDistance ||
                               (distanceToEnemy == closestDistance && highestDamage < damageToEnemy);//More Priority Closest
        if (randomValue < 5000)
        {
            variantFindEnemy = highestDamage < damageToEnemy ||
                               (highestDamage == damageToEnemy && distanceToEnemy < closestDistance); //More Priority Damage
        }

        return variantFindEnemy;
    }
    

    protected override IEnumerator Attack()
    {
        while (true)
        {
            if (!TargetInfo.IsEmpty())
            {
                CreateBullet();
                yield return new WaitForSeconds(attackRite + Random.Range(0, 0.05f));
            }
            else
            {
                FindTarget();
            }

            yield return new WaitForSeconds(0.1f + Random.Range(0, 0.05f));;
        }
        // ReSharper disable once IteratorNeverReturns
    }

    protected override void BulletSetDamage(int targetInstanceID)
    {
        EventManager.TriggerOnSetDamageToEnemy(targetInstanceID, damage, weaponDamageType);
    }
}