using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnTowerWeapon : MonoBehaviour, IListener
{
    [Inject] private TowerWeaponFactory _weaponFactory;
    [Inject] private ResourcesLoader _resourcesLoader;
    [Inject] public EventManager EventManager { get; }

    private List<ShopWeaponItem> _shopWeaponItem;
    
    [Inject]
    public void Initialize()
    {
        _shopWeaponItem = _resourcesLoader.ShopWeaponItems;
    }

    private void BuyItemInShop(string nameWeapon)
    {
        var weaponObject = _shopWeaponItem.Find(item => item.SlotData.Name == nameWeapon);
        
        if (weaponObject)
        {
            _weaponFactory.CreateWeapon(weaponObject.weaponObject.WeaponInfo);
        }
    }
    
    public void OnEnable()
    {
        EventManager.OnNeedCreatePurchasedItem += BuyItemInShop;
    }

    public void OnDisable()
    {
        EventManager.OnNeedCreatePurchasedItem += BuyItemInShop;
    }
}
