using System.Collections;
using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
public class Weapon : MonoBehaviour, IListener
{
    private GameObject _owner;
    private int _ownerID;
    
    [SerializeField] private float weaponRange;
    [SerializeField] private float attackRite;
    [SerializeField] private float damage;

    private int _targetInstanceID;
    
    public float WeaponRange => weaponRange;
    
    [Inject] public EventManager EventManager { get; set; }

    private void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
    }

    public void OnEnable()
    {
        EventManager.OnStopMoveEnemy += EventManagerOnOnStopMoveEnemy;
    }

    public void OnDisable()
    {
        EventManager.OnStopMoveEnemy -= EventManagerOnOnStopMoveEnemy;
    }

    public void SetTarget(int targetId)
    {
        _targetInstanceID = targetId;
    }
    
    private void EventManagerOnOnStopMoveEnemy(int owner)
    {
        if (_ownerID == owner)
        {
            StartCoroutine(Attack());
        }
    }

    protected virtual IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackRite);
            EventManager.TriggerOnSetDamage(_targetInstanceID, damage);
            Debug.Log($"ATTACK {_owner.name}");
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
