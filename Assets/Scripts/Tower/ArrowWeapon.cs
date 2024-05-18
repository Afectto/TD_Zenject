using UnityEngine;

public class ArrowWeapon : TowerWeapon
{
    [SerializeField] private DamageType _type;
    public DamageType DamageType => _type;
}
