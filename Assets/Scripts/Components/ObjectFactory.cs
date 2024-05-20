using UnityEngine;
using Zenject;

public abstract class ObjectFactory
{
    private readonly DiContainer _container;
    
    protected ObjectFactory(DiContainer container)
    {
        _container = container;
    }
    
    protected GameObject Create(GameObject prefab, Vector3 spawnPosition, Quaternion quaternion, Transform parent = null)
    {
        return _container.InstantiatePrefab(prefab, spawnPosition, quaternion, parent);
    }
    
    protected GameObject Create(GameObject prefab, Transform parent)
    {
        return _container.InstantiatePrefab(prefab, parent);
    }
}