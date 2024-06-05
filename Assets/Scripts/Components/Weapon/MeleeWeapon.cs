using System.Collections;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected override IEnumerator Attack()
    {
        while (true)
        {
                yield return new WaitForSeconds(attackRite + Random.Range(0, 0.05f));
                EventManager.TriggerOnSetDamage(TargetInfo.TargetInstanceID, damage);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}