using Zenject;

public class TowerWeaponFactory : ObjectFactory
{
    private readonly Tower _tower;
    
    protected TowerWeaponFactory(Tower tower, DiContainer container) : base(container)
    {
        _tower = tower;
    }

    public void CreateWeapon(WeaponInfo weaponInfo)
    {
        var weapon = Create(weaponInfo.prefab, _tower.WeaponList.transform);
        var towerWeapon = weapon.GetComponent<TowerWeapon>();
        if (towerWeapon)
        {
            towerWeapon.SetFirePoint(_tower.FirePoint);
        }
    }
}