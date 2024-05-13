using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWeapon : TowerWeapon
{
    [SerializeField] private DamageType _type;
    public DamageType DamageType => _type;
}
