using UnityEngine;
using Zenject;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(ArmorBehaviour))]
public class Tower : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}
    
    [SerializeField] private GameObject weaponList;
    [SerializeField] private Transform firePoint;

    public GameObject WeaponList => weaponList;
    public Transform FirePoint => firePoint;

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
