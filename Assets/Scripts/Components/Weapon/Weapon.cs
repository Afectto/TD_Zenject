using System.Collections;
using UnityEngine;
using Zenject;

public struct TargetInfo
{
    private int _targetInstanceID;
    private Transform _targetTransform;

    public int TargetInstanceID => _targetInstanceID;
    public Transform TargetTransform => _targetTransform;
    
    public TargetInfo(int id, Transform target)
    {
        _targetInstanceID = id;
        _targetTransform = target;
    }

    public TargetInfo(GameObject gameObject)
    {
        _targetInstanceID = gameObject.GetInstanceID();
        _targetTransform = gameObject.transform;
    }
    public bool IsEmpty()
    {
        return _targetInstanceID == 0 || _targetTransform == null;
    }
}

// [DisallowMultipleComponent]
public abstract class Weapon : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}
    
    private GameObject _owner;
    private int _ownerID;
    
    [SerializeField] private float weaponRange;
    [SerializeField] protected float attackRite;
    [SerializeField] protected float damage;
    [SerializeField] protected WeaponDamageType weaponDamageType;

    protected TargetInfo TargetInfo;

    public float WeaponRange => weaponRange;
    

    protected virtual void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
    }

    public virtual void OnEnable()
    {
        EventManager.OnStopMoveEnemy += EventManagerOnStopMoveEnemy;
    }

    public virtual  void OnDisable()
    {
        EventManager.OnStopMoveEnemy -= EventManagerOnStopMoveEnemy;
    }

    public void SetTargetInstanceID(TargetInfo targetInfo)
    {
        TargetInfo = targetInfo;
    }

    private void EventManagerOnStopMoveEnemy(int owner)
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
