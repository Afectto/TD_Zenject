using System;
using System.Collections.Generic;
using Zenject;

public class BuffApplier: IDisposable
{
    [Inject]private ResourcesLoader _resourcesLoader;
    [Inject]public EventManager EventManager { get; }

    private Tower _tower;
    private int _towerId;
    
    private List<ShopBuffItem> _shopBuffItems;
    
    [Inject]
    public void Initialize(Tower tower)
    {
        _tower = tower;
        _towerId = _tower.gameObject.GetInstanceID();
        _shopBuffItems = _resourcesLoader.ShopBuffItems;

        EventManager.OnNeedCreatePurchasedItem += BuffAppliedByName;
    }

    private void BuffAppliedByName(string name)
    {
        var buffIndex = _shopBuffItems.FindIndex(item => item.name == name);
        
        if(buffIndex == -1) return;

        var buff = _shopBuffItems[buffIndex].buffObject;
        switch (buff.buffType)
        {
            case BuffType.Tower:
                    AppliedBuffByTower(buff.buffInfo, buff.towerBuffType);
                break;
            case BuffType.TowerWeapon:
                    AppliedBuffByTowerWeapon(buff.buffInfo, buff.weaponDamageType, buff.towerWeaponBuffType);
                break;
            case BuffType.Enemy:
                    AppliedBuffByEnemy(buff.buffInfo, buff.enemyBuffType);
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