using System.Collections;
using UnityEngine;

public class BarrageWeapon : TowerWeapon
{
    [Space(5)]
    [Header("Barrage Component")]
    [SerializeField] private int bulletCountInOneShot;
    [SerializeField] private float delay;
    
    protected override IEnumerator Attack()
    {
        while (true)
        {
            if (!TargetInfo.IsEmpty())
            {
                for (int i = 0; i < bulletCountInOneShot; i++)
                {
                    CreateBullet();
                    yield return new WaitForSeconds(delay + Random.Range(0, 0.05f));;
                }
                yield return new WaitForSeconds(attackRite + Random.Range(0, 0.05f));
            }
            else
            {
                FindTarget();
            }

            yield return new WaitForSeconds(0.1f + Random.Range(0, 0.05f));;
        }
        // ReSharper disable once IteratorNeverReturns
    }

}
