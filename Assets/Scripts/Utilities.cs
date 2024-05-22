using System;
using UnityEngine;

public static class Utilities
{
    public static void DoForEachEnemyInRadius(Vector3 center, float radius, Action<GameObject> func)
    {
        DoForEachTagObjectInRadius("Enemy", center, radius, func);
    }

    public static void DoForEachTagObjectInRadius(string tag , Vector3 center, float radius, Action<GameObject> func)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(tag))
            {
                func(collider.gameObject);
            }
        }
    }
}
