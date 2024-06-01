using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public struct ArmorReductionConfiguration
{
    public Dictionary<ArmorType, float> ArmorMultiplier;
}

public class Armor : IInitializable
{
    private Dictionary<WeaponDamageType, ArmorReductionConfiguration> _armorReduction;
    public IReadOnlyDictionary<WeaponDamageType, ArmorReductionConfiguration> ArmorReductionConfigurations => _armorReduction;

    [Inject]
    public void Initialize()
    {
        _armorReduction = new Dictionary<WeaponDamageType, ArmorReductionConfiguration>();

        AddArmorReductionConfiguration(WeaponDamageType.Pierce, 1.5f, 2,     0.75f, 0.9f, 0.4f, 0.9f);
        AddArmorReductionConfiguration(WeaponDamageType.Siege,  1.1f, 1,     0.6f,  1,    1.5f, 0.9f);
        AddArmorReductionConfiguration(WeaponDamageType.Magic,  1.1f, 1.25f, 0.7f,  2,    0.4f, 0.9f);
        AddArmorReductionConfiguration(WeaponDamageType.Normal, 1.5f, 1.2f,  1.5f,  1,    0.7f, 0.9f);
        AddArmorReductionConfiguration(WeaponDamageType.Corrupt,1.3f, 1.2f,  1.2f,  1.2f, 0.9f, 1f);
    }
    
    private void AddArmorReductionConfiguration(WeaponDamageType weaponType, 
        float unarmored, float light, float medium, float heavy, float fortified, float epic)
    {
        _armorReduction.Add(weaponType, new ArmorReductionConfiguration
        {
            ArmorMultiplier = new Dictionary<ArmorType, float>
            {
                {ArmorType.Unarmored, unarmored},
                {ArmorType.Light, light},
                {ArmorType.Medium, medium},
                {ArmorType.Heavy, heavy},
                {ArmorType.Fortified, fortified},
                {ArmorType.Epic, epic},
            }
        });
    }
    
    public float CalculateDamage(float damage, WeaponDamageType weaponDamageType, ArmorType armorType, float armorValue)
    {
        if (weaponDamageType == WeaponDamageType.None || armorType == ArmorType.None)
        {
            throw new Exception("WeaponDamageType or armorType is None");
        }
        
        float armorMultiplier = _armorReduction[weaponDamageType].ArmorMultiplier[armorType];
        float damageResults = damage * armorMultiplier;
        
        return CalculateDamageReduction(armorValue, damageResults);
    }

    public float CalculateDamageReduction(float armorValue, float damageResults)
    {
        if (armorValue > 0)
        {
            float damageReduction = (armorValue * 0.06f) / (1 + 0.06f * armorValue);
            damageResults *= 1 - damageReduction;
        }
        else
        {
            float damageIncrease = 2 - Mathf.Pow(0.94f, -armorValue);;
            damageResults *= damageIncrease;
        }

        return damageResults;
    }
}