using System;
using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
public class ArmorBehaviour : MonoBehaviour, IListener
{
    [Inject] private Armor _armor;
    [Inject] public EventManager EventManager { get; }
    
    [SerializeField] private ArmorType armorType;
    [SerializeField] private float armorValue;
    
    private GameObject _owner;
    private int _ownerID;
    
    public ArmorType ArmorType => armorType;
    public float Armor => armorValue;

    private void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
    }

    public void OnEnable()
    {
        if (GetComponent<Tower>() != null)
        {
            EventManager.OnAddArmorToTower += AddArmorToTower;
        }
    }

    private void AddArmorToTower(int owner, float value)
    {
        if (_ownerID == owner)
        {
            armorValue += value;
        }
    }

    public float GetDamageReducedByArmor(float damage, WeaponDamageType weaponDamageType)
    {
        return _armor.CalculateDamage(damage, weaponDamageType, armorType, armorValue);
    }
    
    public float GetDamageReducedByArmor(float damage)
    {
        return _armor.CalculateDamageReduction(armorValue, damage);
    }

    public void OnDisable()
    {
        EventManager.OnAddArmorToTower -= AddArmorToTower;
    }
}