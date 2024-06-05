using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyFactory : ObjectFactory
{
    private const float XMin = -13f;
    private const float XMax = 13f;
    private const float YMin = -6f;
    private const float YMax = 6f;
    private const float AvoidanceRadius = 7f;

    public EnemyFactory(DiContainer container) : base(container)
    {
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

    private void CreateEnemy(Vector2 groupPosition, GameObject enemyPrefab)
    {        
        Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Vector2 spawnPosition = groupPosition + offset;
        Create(enemyPrefab, spawnPosition, quaternion.identity);
    }

    private Vector3 GenerateRandomPosition()
    {
        float randX, randY;
        do
        {
            randX = Random.Range(XMin, XMax);
            randY = Random.Range(YMin, YMax);
        } while (Vector2.Distance(new Vector2(randX, randY), Vector2.zero) < AvoidanceRadius);

        return new Vector3(randX, randY, 0);
    }

}