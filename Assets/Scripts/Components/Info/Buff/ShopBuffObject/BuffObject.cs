using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class BuffObject 
{
    public BuffInfo buffInfo;
    
    public BuffType buffType;
    
    public TowerBuffType towerBuffType;
    public TowerWeaponBuffType towerWeaponBuffType;
    public WeaponDamageType weaponDamageType;
    public EnemyBuffType enemyBuffType;

    public BuffObject(BuffObject buffObject)
    {
        buffInfo = buffObject.buffInfo;
        
        buffType = buffObject.buffType;
        
        towerBuffType = buffObject.towerBuffType;
        towerWeaponBuffType = buffObject.towerWeaponBuffType;
        weaponDamageType = buffObject.weaponDamageType;
        enemyBuffType = buffObject.enemyBuffType;
    }
}