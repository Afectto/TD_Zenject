using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerWeaponMultiplayer : IInitializable, IDisposable
{
    [Inject] public EventManager EventManager { get;}

    private Dictionary<WeaponDamageType, WeaponMultiplayerInfo> _weaponMultiplayerByType;
    
    public event Action<WeaponDamageType> OnAddDamageBuff;
    public event Action<WeaponDamageType> OnAddSpeedBuff;

    [Inject]
    public void Initialize()
    {
        _weaponMultiplayerByType = new Dictionary<WeaponDamageType, WeaponMultiplayerInfo>();
        WeaponDamageType[] enumValues = (WeaponDamageType[])Enum.GetValues(typeof(WeaponDamageType));
        int count = enumValues.Length;
        for (int i = 0; i < count; i++)
        {
            _weaponMultiplayerByType.Add((WeaponDamageType)i, new WeaponMultiplayerInfo
            {
                CurrentDamageMultiplayer = 1,
                CurrentAttackRiteMultiplayer = 1
            });
        }
        
        EventManager.OnAddDamageBuffToTowerWeapon += AddDamage;
        EventManager.OnAddSpeedBuffToTowerWeapon += AddAttackRite;
        
    }
    
    private void AddDamage(float value, WeaponDamageType damageType)
    {
        _weaponMultiplayerByType[damageType].CurrentDamageMultiplayer += value / 100;
        OnAddDamageBuff?.Invoke(damageType);
    }

    private void AddAttackRite(float value, WeaponDamageType damageType)
    {
        _weaponMultiplayerByType[damageType].CurrentAttackRiteMultiplayer += value / 100;
        OnAddSpeedBuff?.Invoke(damageType);
    }

    public float GetDamageMultiplayer(WeaponDamageType damageType)
    {
        return _weaponMultiplayerByType[damageType].CurrentDamageMultiplayer;
    }
    
    public float GetAttackRiteMultiplayer(WeaponDamageType damageType)
    {
        return _weaponMultiplayerByType[damageType].CurrentAttackRiteMultiplayer;
    }


    public void Dispose()
    {
        EventManager.OnAddDamageBuffToTowerWeapon += AddDamage;
        EventManager.OnAddSpeedBuffToTowerWeapon += AddAttackRite;
    }
}
