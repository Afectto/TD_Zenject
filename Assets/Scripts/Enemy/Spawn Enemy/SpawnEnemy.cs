using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnEnemy : MonoBehaviour
{
    [Inject] private EnemyFactory _enemyFactory;
    [Inject] private ResourcesLoader _resourcesLoader;
    
    private List<EnemyGroup> _enemyGroups;
    
    [SerializeField] private float spawnDelay = 5;
    
    void Start()
    {
        AddAllEnemyGroup();
        
        StartCoroutine(Spawn());
    }
    
    private void AddAllEnemyGroup()
    {
        _enemyGroups = new List<EnemyGroup>();

        var allEnemyGroup = _resourcesLoader.EnemyGroups;
        foreach (var grGroup in allEnemyGroup)
        {
            _enemyGroups.Add(grGroup);
        }
    }
    
    private IEnumerator Spawn()
    {
        while(true)
        {
            SpawnRandomEnemyGroup();
            yield return new WaitForSeconds(spawnDelay);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void SpawnRandomEnemyGroup()
    {        
        var randomGroupNumber = Random.Range(0, _enemyGroups.Count);
        var enemyGroup = _enemyGroups[randomGroupNumber];
        _enemyFactory.CreateEnemyGroup(enemyGroup.EnemyInfos);
    }
    
}
