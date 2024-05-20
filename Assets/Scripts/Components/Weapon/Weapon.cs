using System.Collections;
using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
public abstract class Weapon : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}
    
    private GameObject _owner;
    private int _ownerID;
    
    [SerializeField] private float weaponRange;
    [SerializeField] protected float attackRite;
    [SerializeField] protected float damage;
    [SerializeField] protected WeaponDamageType weaponDamageType;

    protected int TargetInstanceID;
    protected Transform TargetTransform;
    
    public float WeaponRange => weaponRange;
    

    protected virtual void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
    }

    public virtual void OnEnable()
    {
        EventManager.OnStopMoveEnemy += EventManagerOnOnStopMoveEnemy;
    }

    public virtual  void OnDisable()
    {
        EventManager.OnStopMoveEnemy -= EventManagerOnOnStopMoveEnemy;
    }

    public void SetTargetInstanceID(int targetId, Transform target)
    {
        TargetInstanceID = targetId;
        TargetTransform = target;
    }

    private void EventManagerOnOnStopMoveEnemy(int owner)
    {
        if (_ownerID == owner)
        {
            StartCoroutine(Attack());
        }
    }

    protected abstract IEnumerator Attack();
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, WeaponRange);
    }
#endif
}
