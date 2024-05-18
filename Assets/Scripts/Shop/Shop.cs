using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    [Inject] private TowerWeaponFactory _towerWeaponFactory;

    [SerializeField] private Slot slotPrefab;
    private List<ShopBuffItem> _buffItems;
    private List<ShopWeaponItem> _weaponItems;
    private DiContainer _container;
    
    private const int BUFF_COUNT = 3;
    private const int WEAPON_COUNT = 3;
    
    [Inject]
    public void Initialize(DiContainer diContainer)
    {
        _container = diContainer;
        _buffItems = Resources.LoadAll<ShopBuffItem>("ScriptableObject/ShopItem/BuffItem").ToList();
        _weaponItems = Resources.LoadAll<ShopWeaponItem>("ScriptableObject/ShopItem/WeaponItem").ToList();
        CreateBuffRow();
        CreateWeaponRow();
    }

    private void Awake()
    {

    }

    private void CreateBuffRow()
    {
        for (int i = 0; i < BUFF_COUNT; i++)
        {
            var item = GetRandomBuffItem();
            CreateSlot(item.SlotData, i);
        }
    }
    
    private void CreateWeaponRow()
    {
        for (int i = 0; i < WEAPON_COUNT; i++)
        {
            var item = GetGetRandomWeaponItem();
            CreateSlot(item.SlotData, i, false);
        }
    }


    private void CreateSlot(ISlotItem slotData, int count, bool isTop = true)
    {
        var position = transform.position;
        var posX = position.x - 70 * count;
        var posY = position.y + (isTop ? 70 : 0) ;
        var slotGameObject = _container.InstantiatePrefab(slotPrefab, transform);
        var slot = slotGameObject.GetComponent<Slot>();
        slot.transform.localScale = slotPrefab.transform.localScale;
        slot.transform.localPosition = new Vector3(posX, posY, 0);
        slot.SetSlotData(slotData);
    }
    
    private ShopBuffItem GetRandomBuffItem()
    {
        var randomIndex = Random.Range(0, _buffItems.Count-1);
        return _buffItems[randomIndex];
    }

    private ShopWeaponItem GetGetRandomWeaponItem()
    {
        var randomIndex = Random.Range(0, _weaponItems.Count-1);
        return _weaponItems[randomIndex];
    }
    
}
