using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ResourcesLoader : IInitializable
{
    public List<ShopWeaponItem> ShopWeaponItems { get; private set; }
    public List<ShopBuffItem> ShopBuffItems { get; private set; }
    public List<EnemyGroup> EnemyGroups { get; private set; }

    [Inject]
    public void Initialize()
    {
        LoadScriptableObject();
    }

    private void LoadScriptableObject()
    {
        ShopWeaponItems = Resources.LoadAll<ShopWeaponItem>("ScriptableObject/ShopItem/WeaponItem").ToList();
        ShopBuffItems = Resources.LoadAll<ShopBuffItem>("ScriptableObject/ShopItem/BuffItem").ToList();
        EnemyGroups = Resources.LoadAll<EnemyGroup>("ScriptableObject/EnemyGroup").ToList();
    }
}
