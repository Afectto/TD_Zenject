using UnityEngine;
using Zenject;

[RequireComponent(typeof(Health))]
public class Tower : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}
    
    private void Awake()
    {
        AddToInspector();
    }
    
    private void AddToInspector()
    {
        if (!Application.isPlaying)
        {
            Tower tower = GetComponent<Tower>();
            if (tower != null )
            {
                tower.gameObject.AddComponent<Health>();
            }
        }
    }
    
    public void OnEnable()
    {
        if (EventManager != null)
        {
            EventManager.OnDeath += OnDeath;
        }
    }

    public void OnDisable()
    {
        if (EventManager != null)
        {
            EventManager.OnDeath -= OnDeath;
        }
    }
    
    private void OnDeath(int owner)
    {
        if (GetInstanceID() == owner)
        {
            EventManager.TriggerOnTowerDestroy();
        }
    }

}
