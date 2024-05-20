using UnityEngine;

[CreateAssetMenu(fileName = "BuffObject", menuName = "GameInfo/New BuffObject")]
public class BuffObject : ScriptableObject
{
    public string Name;
    public BuffInfo BuffInfo;
    
    public BuffType BuffType;
    
    public EnemyBuffType EnemyBuffType;
    public TowerBuffType TowerBuffType;
    public TowerWeaponBuffType TowerWeaponBuffType;
}