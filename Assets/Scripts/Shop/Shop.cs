using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    [Inject] private MoneyManager _moneyManager;
    [Inject] private EventManager _eventManager;

    [SerializeField] private Slot slotPrefab;
    private List<ShopBuffItem> _buffItems;
    private List<ShopWeaponItem> _weaponItems;
    private DiContainer _container;

    private List<Slot> _buffSlots;
    private List<Slot> _weaponSlots;

    [SerializeField] private TextMeshProUGUI refreshValueText;
    private int _refreshValue = 100;
    
    private const int BUFF_COUNT = 3;
    private const int WEAPON_COUNT = 3;
    
    [Inject]
    public void Initialize(DiContainer diContainer)
    {
        _container = diContainer;
        _buffItems = Resources.LoadAll<ShopBuffItem>("ScriptableObject/ShopItem/BuffItem").ToList();
        _weaponItems = Resources.LoadAll<ShopWeaponItem>("ScriptableObject/ShopItem/WeaponItem").ToList();
        
        _buffSlots = new List<Slot>();
        _weaponSlots = new List<Slot>();
        
        refreshValueText.text = _refreshValue.ToString();
        
        CreateBuffRow();
        CreateWeaponRow();
    }
    
    private void CreateBuffRow()
    {
        for (int i = 0; i < BUFF_COUNT; i++)
        {
            var item = GetRandomBuffItem();
            _buffSlots.Add(CreateSlot(item.SlotData, i));
        }
    }
    
    private void CreateWeaponRow()
    {
        for (int i = 0; i < WEAPON_COUNT; i++)
        {
            var item = GetGetRandomWeaponItem();
            _weaponSlots.Add(CreateSlot(item.SlotData, i, false));
        }
    }


    private Slot CreateSlot(ISlotItem slotData, int count, bool isTop = true)
    {
        var position = transform.position;
        var posX = position.x - 70 * count;
        var posY = position.y + (isTop ? 70 : 0) ;
        var slotGameObject = _container.InstantiatePrefab(slotPrefab, transform);
        var slot = slotGameObject.GetComponent<Slot>();
        slot.transform.localScale = slotPrefab.transform.localScale;
        slot.transform.localPosition = new Vector3(posX, posY, 0);
        slot.SetSlotData(slotData);
        return slot;
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

    public void UpdateShop()
    {
        if(_moneyManager.CurrentMoney <= _refreshValue) return;;
        _eventManager.TriggerOnBuyItemInShop(_refreshValue);
        _refreshValue += 100;
        refreshValueText.text = _refreshValue.ToString();
        
        foreach (var slot in _buffSlots)
        {
            UpdateSlot(slot, true);
        }
        
        foreach (var slot in _weaponSlots)
        {
            UpdateSlot(slot, false);
        }
    }

    private void UpdateSlot(Slot slot, bool isBuff)
    {
        SlotData slotData = isBuff ? GetRandomBuffItem().SlotData : GetGetRandomWeaponItem().SlotData;
        slot.SetSlotData(slotData);
    }
}
