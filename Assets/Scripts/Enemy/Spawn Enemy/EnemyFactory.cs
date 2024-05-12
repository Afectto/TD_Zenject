using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyFactory 
{
    private readonly DiContainer _container;
    
    private float xMin = -13f;
    private float xMax = 13f;
    private float yMin = -6f;
    private float yMax = 6f;
    private float avoidanceRadius = 7f;
    
    public EnemyFactory(DiContainer container)
    {
        _container = container;
    }

    public void Create(Vector3 spawnPosition, GameObject prefab)
    {
        _container.InstantiatePrefab(prefab, spawnPosition, quaternion.identity, null);
    }
    
    public void CreateEnemyGroup(List<EnemyInfo> enemyInfos)
    {
        if(enemyInfos.Count == 0) 
        {
            Debug.LogError("Invalid parameters for CreateEnemyGroup.");
            return;
        }
        Vector3 groupPosition = GenerateRandomPosition();

        foreach (var enemyInfo in enemyInfos)
        {
            for (int j = 0; j < enemyInfo.countInGroup; j++) 
            { 
                CreateEnemy(groupPosition, enemyInfo.enemyPrefab);
            }
        }
    }

    private void CreateEnemy(Vector3 groupPosition, GameObject enemyPrefab)
    {        
        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
        Vector3 spawnPosition = groupPosition + offset;
        Create(spawnPosition, enemyPrefab);
    }


    private Vector3 GenerateRandomPosition()
    {
        float randX, randY;
        do
        {
            randX = Random.Range(xMin, xMax);
            randY = Random.Range(yMin, yMax);
        } while (Vector2.Distance(new Vector2(randX, randY), Vector2.zero) < avoidanceRadius);

        return new Vector3(randX, randY, 0);
    }
}

