using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WeaponObject", menuName = "GameInfo/New WeaponObject")]
public class WeaponObject : ScriptableObject
{
    public string Name;
    public WeaponInfo WeaponInfo;
}