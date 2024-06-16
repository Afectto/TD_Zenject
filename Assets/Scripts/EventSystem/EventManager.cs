using System;
using UnityEngine;

public class EventManager
{
    //MOVE
    public event Action<int, float, Vector3> MoveCommand;
    public void TriggerMoveCommand(int owner, float stopDistance, Vector3 target)
    {
        MoveCommand?.Invoke(owner, stopDistance, target);
    }

    public event Action<int> OnStopMoveEnemy;
    public void TriggerOnStopMoveEnemy(int owner)
    {
        OnStopMoveEnemy?.Invoke(owner);
    }

    //DAMAGE
    public event Action<int, float> OnSetDamage;
    public void TriggerOnSetDamage(int target, float damage)
    {
        OnSetDamage?.Invoke(target, damage);
    }

    public event Action<int, float, WeaponDamageType> OnSetDamageToEnemy;
    public void TriggerOnSetDamageToEnemy(int target, float damage, WeaponDamageType weaponDamageType)
    {
        OnSetDamageToEnemy?.Invoke(target, damage, weaponDamageType);
    }
    
    public event Action<int> OnDeath;
    public void TriggerOnDeath(int owner)
    {
        OnDeath?.Invoke(owner);
    }
    
    //GAME STATE
    public event Action OnTowerDestroy;
    public void TriggerOnTowerDestroy()
    {
        OnTowerDestroy?.Invoke();
    }
    
    //SHOP
    public event Action<string> OnNeedCreatePurchasedItem;
    public void TriggerOnNeedCreatePurchasedItem(string name)
    {
        OnNeedCreatePurchasedItem?.Invoke(name);
    }

    public event Action<int> OnBuyItemInShop;
    public void TriggerOnBuyItemInShop(int price)
    {
        OnBuyItemInShop?.Invoke(price);
    }
    
    //MONEY MANAGER
    public event Action OnChangeMoney;
    public void TriggerOnChangeMoney()
    {
        OnChangeMoney?.Invoke();
    }

    public event Action<float> OnRewardByEnemy; 
    public void TriggerOnRewardByEnemy(float value)
    {
        OnRewardByEnemy?.Invoke(value);
    }
    //UPDATE TIMER
    public event Action OnNeedUpdateShop;
    public void TriggerOnNeedUpdateShop()
    {
        OnNeedUpdateShop?.Invoke();
    }
    
    //BUFF APPLIED

    #region Tower
    public event Action<int, float> OnAddArmorToTower;
    public void TriggerOnAddArmorToTower(int towerId, float value)
    {
        OnAddArmorToTower?.Invoke(towerId, value);
    }
    
    public event Action<int, float> OnAddHealthToTower;
    public void TriggerOnAddHealthToTower(int towerId, float value)
    {
        OnAddHealthToTower?.Invoke(towerId, value);
    }
    
    public event Action<int, float> OnAddRegenToTower;
    public void TriggerOnAddRegenToTower(int towerId, float value)
    {
        OnAddRegenToTower?.Invoke(towerId, value);
    }
    
    public event Action<float> OnAddIncome;
    public void TriggerOnAddIncome(float value)
    {
        OnAddIncome?.Invoke(value);
    }
    #endregion

    #region TowerWeapon
    public event Action<float,WeaponDamageType> OnAddDamageBuffToTowerWeapon;
    public void TriggerOnAddDamageBuffToTowerWeapon(float value, WeaponDamageType damageType)
    {
        OnAddDamageBuffToTowerWeapon?.Invoke(value, damageType);
    }
    
    public event Action<float,WeaponDamageType> OnAddSpeedBuffToTowerWeapon;
    public void TriggerOnAddSpeedBuffToTowerWeapon(float value, WeaponDamageType damageType)
    {
        OnAddSpeedBuffToTowerWeapon?.Invoke(value, damageType);
    }
    
    #endregion

    #region Enemy



    #region EnemyStatus
    public event Action<StatusEffectType, int> OnSetStatus;
    public void TriggerOnSetStatus(StatusEffectType obj, int target)
    {
        OnSetStatus?.Invoke(obj, target);
    }
    
    public event Action<IStatusEffect> OnRemoveActiveStatus;
    public void TriggerOnRemoveActiveStatus(IStatusEffect obj)
    {
        OnRemoveActiveStatus?.Invoke(obj);
    }

    public event Action<int, float> OnChangeIceStatus;
    public void TriggerOnChangeIceStatus(int target, float value)
    {
        OnChangeIceStatus?.Invoke(target, value);
    }

    #endregion

    #endregion

}
