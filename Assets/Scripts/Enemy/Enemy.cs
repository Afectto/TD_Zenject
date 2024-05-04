using UnityEngine;
using Zenject;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(MoveToTarget))]
[DisallowMultipleComponent]
[ExecuteInEditMode]
public class Enemy : MonoBehaviour, IListener
{
    private Weapon _weapon;
   [Inject] public EventManager EventManager { get; set; }
    
    private void Awake()
    {
        AddToInspector();

        _weapon = GetComponent<Weapon>();
    }

    private void AddToInspector()
    {
        if (!Application.isPlaying)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy != null )
            {
                enemy.gameObject.AddComponent<Health>();
                enemy.gameObject.AddComponent<Weapon>();
                enemy.gameObject.AddComponent<MoveToTarget>();
            }
        }
    }
    
    private void Start()
    {
        EventManager?.TriggerMoveCommand(gameObject.GetInstanceID(), _weapon.WeaponRange, Vector3.zero);
        
        _weapon.SetTarget(FindObjectOfType<Tower>().gameObject.GetInstanceID());
    }

    public void OnEnable()
    {
        if (EventManager != null)
        {
            EventManager.OnDeath += OnDeath;
        }
    }

    private void OnDeath(int owner)
    {
        if (gameObject.GetInstanceID() == owner)
        {
            Destroy(gameObject);
        }
    }

    public void OnDisable()
    {
        if (EventManager != null)
        {
            EventManager.OnDeath -= OnDeath;
        }
    }
}