using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    protected override IEnumerator Attack()
    {
        while (true)
        {
                yield return new WaitForSeconds(attackRite);
                EventManager.TriggerOnSetDamage(TargetInstanceID, damage);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}