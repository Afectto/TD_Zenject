using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    [Inject] private TowerWeaponFactory _towerWeaponFactory;
    
    [SerializeField] private WeaponInfo _info;

    public void AddWeapon()
    {
        _towerWeaponFactory.CreateWeapon(_info);
    }
}
