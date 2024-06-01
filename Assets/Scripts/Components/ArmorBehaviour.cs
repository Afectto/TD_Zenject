using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
public class ArmorBehaviour : MonoBehaviour
{
    [Inject] private Armor _armor;
    
    [SerializeField] private ArmorType armorType;
    [SerializeField] private float armorValue;
    
    public ArmorType ArmorType => armorType;
    public float Armor => armorValue;

    public float GetDamageReducedByArmor(float damage, WeaponDamageType weaponDamageType)
    {
        return _armor.CalculateDamage(damage, weaponDamageType, armorType, armorValue);
    }
}