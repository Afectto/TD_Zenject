using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ResourcesLoader : IInitializable
{
    private List<ShopWeaponItem>  _shopWeaponItems;
    public List<ShopWeaponItem> ShopWeaponItems
    {
        get => _shopWeaponItems;
        private set => _shopWeaponItems = value;
    }
    
    private List<ShopBuffItem>  _shopBuffItems;
    public List<ShopBuffItem> ShopBuffItems
    {
        get => _shopBuffItems;
        private set => _shopBuffItems = value;
    }    
    
    private List<EnemyGroup>  _enemyGroup;
    public List<EnemyGroup> EnemyGroups
    {
        get => _enemyGroup;
        private set => _enemyGroup = value;
    }
    
    [Inject]
    public void Initialize()
    {
        ShopWeaponItems = Resources.LoadAll<ShopWeaponItem>("ScriptableObject/ShopItem/WeaponItem").ToList();
        ShopBuffItems = Resources.LoadAll<ShopBuffItem>("ScriptableObject/ShopItem/BuffItem").ToList();
        EnemyGroups = Resources.LoadAll<EnemyGroup>("ScriptableObject/EnemyGroup").ToList();
    }
}
