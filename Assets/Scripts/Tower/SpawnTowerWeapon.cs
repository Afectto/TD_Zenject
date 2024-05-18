using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpawnTowerWeapon : MonoBehaviour
{
    [Inject] private TowerWeaponFactory _weaponFactory;
    [Inject] private EventManager _eventManager;

    private List<WeaponObject> _weaponObjects;
    
    [Inject]
    public void Instance()
    {
        _weaponObjects = Resources.LoadAll<WeaponObject>("ScriptableObject/WeaponObject").ToList();
        _eventManager.OnClickShopSlot += BuyItemInShop;
    }

    private void BuyItemInShop(string nameWeapon)
    {
        var weaponObject = _weaponObjects.Find(item => item.Name == nameWeapon);
        if (weaponObject)
        {
            _weaponFactory.CreateWeapon(weaponObject.WeaponInfo);
        }
    }

    private void OnDestroy()
    {
        _eventManager.OnClickShopSlot += BuyItemInShop;
    }
}
