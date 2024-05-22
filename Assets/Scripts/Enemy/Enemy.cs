using UnityEngine;
using Zenject;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MoveToTarget))]
[DisallowMultipleComponent]
[ExecuteInEditMode]
public abstract class Enemy : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}
    [SerializeField] private float reward;
    
    private Weapon _weapon;
    
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
                enemy.gameObject.AddComponent<MoveToTarget>();
            }
        }
    }
    
    private void Start()
    {
        EventManager?.TriggerMoveCommand(gameObject.GetInstanceID(), _weapon.WeaponRange, Vector3.zero);

        var tower = FindObjectOfType<Tower>().gameObject;
        _weapon.SetTargetInstanceID(tower.GetInstanceID(), tower.transform);
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
            EventManager?.TriggerOnRewardByEnemy(reward);
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