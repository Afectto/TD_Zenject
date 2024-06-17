using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotWeapon : TowerWeapon
{
    [Space(5)]
    [Header("Multishot Component")]
    [SerializeField]private int maxTargetCount;
    private List<TargetInfo> _targetsInfo;

    public override void OnValidate()
    {
        totalDPS = (damage / attackRite * maxTargetCount).ToString();
    }

    protected override void Awake()
    {
        _targetsInfo = new List<TargetInfo>();
        base.Awake();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnDeath += RemoveTargetFromList;
    }

    private void RemoveTargetFromList(int target)
    {
        var targetInfo = _targetsInfo.Find(info => info.TargetInstanceID == target);
        if (!targetInfo.IsEmpty())
        {
            _targetsInfo.Remove(targetInfo);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnDeath -= RemoveTargetFromList;
    }

    protected override void FindTarget()
    {        
        List<GameObject> enemy = new List<GameObject>();
        float highestDamage = 0.0f;
        float closestDistance = float.MaxValue;

        Utils.ForEachEnemyInRadius(transform.position, WeaponRange, enemyObject =>
        {
            var armorBehaviour = enemyObject.GetComponent<ArmorBehaviour>();

            var damageToEnemy = _armor.CalculateDamage(damage, weaponDamageType, armorBehaviour.ArmorType, armorBehaviour.Armor);
            float distanceToEnemy = Vector3.Distance(transform.position, enemyObject.transform.position);

            bool variantFindEnemy = GetVariantFindEnemy(distanceToEnemy, closestDistance, damageToEnemy, highestDamage);
            
            if (variantFindEnemy && !enemy.Contains(enemyObject))
            {
                enemy.Add(enemyObject);
                highestDamage = damageToEnemy;
                closestDistance = distanceToEnemy;
            }
        });

        foreach (var enemyGameObject in enemy)
        {
            TargetInfo info = new TargetInfo(enemyGameObject);
            if (_targetsInfo.Count < maxTargetCount && !_targetsInfo.Contains(info))
            {
                _targetsInfo.Add(info);
            }
        }
    }
    
    protected override IEnumerator Attack()
    {
        while (true)
        {
            if (_targetsInfo.Count > 0 )
            {
                foreach (var info in _targetsInfo)
                { 
                    CreateBullet(info);
                }
                yield return new WaitForSeconds(attackRite + Random.Range(0, 0.05f));
                if (_targetsInfo.Count < maxTargetCount)
                {
                    FindTarget();
                }
            }
            else 
            {
                FindTarget();
            }
            
            yield return new WaitForSeconds(0.1f + Random.Range(0, 0.05f));; 
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
