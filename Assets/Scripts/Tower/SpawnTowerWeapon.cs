using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SpawnTowerWeapon : MonoBehaviour, IListener
{
    [Inject] private TowerWeaponFactory _weaponFactory;
    [Inject] public EventManager EventManager { get; }

    private List<WeaponObject> _weaponObjects;
    
    [Inject]
    public void Instance()
    {
        _weaponObjects = Resources.LoadAll<WeaponObject>("ScriptableObject/WeaponObject").ToList();
    }

    private void BuyItemInShop(string nameWeapon)
    {
        var weaponObject = _weaponObjects.Find(item => item.Name == nameWeapon);
        if (weaponObject)
        {
            _weaponFactory.CreateWeapon(weaponObject.WeaponInfo);
        }
    }
    
    public void OnEnable()
    {
        EventManager.OnClickShopSlot += BuyItemInShop;
    }

    public void OnDisable()
    {
        EventManager.OnClickShopSlot += BuyItemInShop;
    }
}
