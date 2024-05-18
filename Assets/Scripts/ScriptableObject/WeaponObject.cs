using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponObject", menuName = "GameInfo/New WeaponObject")]
public class WeaponObject : ScriptableObject
{
    public string Name;
    public WeaponInfo WeaponInfo;
}