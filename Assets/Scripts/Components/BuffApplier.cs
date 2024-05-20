using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BuffApplier: IDisposable
{
    [Inject]public EventManager EventManager { get; }

    private Tower _tower;
    private int _towerId;
    private List<BuffObject> _allBuffs;
    
    [Inject]
    public void Initialize(Tower tower)
    {
        _tower = tower;
        _towerId = _tower.gameObject.GetInstanceID();
        _allBuffs =  Resources.LoadAll<BuffObject>("ScriptableObject/BuffObject").ToList();
        EventManager.OnNeedCreatePurchasedItem += BuffAppliedByName;
    }

    private void BuffAppliedByName(string name)
    {
        var buff = _allBuffs.Find(item => item.Name == name);
        
        if(!buff) return;
        
        switch (buff.BuffType)
        {
            case BuffType.Tower:
                    AppliedBuffByTower(buff.BuffInfo, buff.TowerBuffType);
                break;
            case BuffType.TowerWeapon:
                    AppliedBuffByTowerWeapon(buff.BuffInfo, buff.WeaponDamageType, buff.TowerWeaponBuffType);
                break;
            case BuffType.Enemy:
                    AppliedBuffByEnemy(buff.BuffInfo, buff.EnemyBuffType);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void AppliedBuffByTower(BuffInfo buffBuffInfo, TowerBuffType buffTowerBuffType)
    {
        switch (buffTowerBuffType)
        {
            case TowerBuffType.Armor:
                    EventManager.TriggerOnAddArmorToTower(_towerId, buffBuffInfo.Value);
                break;
            case TowerBuffType.Health:
                    EventManager.TriggerOnAddHealthToTower(_towerId, buffBuffInfo.Value);
                break;
            case TowerBuffType.Regen:
                    EventManager.TriggerOnAddRegenToTower(_towerId, buffBuffInfo.Value);
                break;
            case TowerBuffType.Income:
                    EventManager.TriggerOnAddIncome(buffBuffInfo.Value);
                break;
        }
    }
    
    private void AppliedBuffByTowerWeapon(BuffInfo buffBuffInfo, WeaponDamageType damageType, TowerWeaponBuffType buffTowerWeaponBuffType)
    {        
        switch (buffTowerWeaponBuffType)
        {
            case TowerWeaponBuffType.Damage:
                EventManager.TriggerOnAddDamageBuffToTowerWeapon(buffBuffInfo.Value, damageType);
                break;
            case TowerWeaponBuffType.Speed:
                EventManager.TriggerOnAddSpeedBuffToTowerWeapon(buffBuffInfo.Value, damageType);
                break;
        }
    }

    private void AppliedBuffByEnemy(BuffInfo buffBuffInfo, EnemyBuffType buffEnemyBuffType)
    {        
        switch (buffEnemyBuffType)
        {
            case EnemyBuffType.Armor:
                break;
            case EnemyBuffType.Health:
                break;
            case EnemyBuffType.Regen:
                break;
        }
    }

    public void Dispose()
    {
        EventManager.OnNeedCreatePurchasedItem -= BuffAppliedByName;
    }
}