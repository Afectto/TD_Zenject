using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void ForEachEnemyInRadius(Vector3 center, float radius, Action<GameObject> func)
    {
        ForEachTagObjectInRadius("Enemy", center, radius, func);
    }

    public static void ForEachTagObjectInRadius(string tag , Vector3 center, float radius, Action<GameObject> func)
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

    public static List<Collider2D> GetAllTagObjectInRadius(string tag , Vector3 center, float radius)
    {
        List<Collider2D> rezult = new List<Collider2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(tag))
            {
                rezult.Add(collider);
            }
        }

        return rezult;
    }

    public class Coroutines : MonoBehaviour { }
}


