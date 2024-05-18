using System.Collections;
using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
public class MoveToTarget : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}
    
    private GameObject _owner;
    private int _ownerID;
    
    [SerializeField] private float speed;

    private void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
    }

    public void OnEnable()
    {
        EventManager.MoveCommand += OnMoveToTarget;
    }

    public void OnDisable()
    {
        EventManager.MoveCommand -= OnMoveToTarget;
    }

    private void OnMoveToTarget(int owner, float stopDistance, Vector3 target)
    {
        if (_ownerID == owner)
        {
            StartCoroutine(MoveCoroutine(stopDistance, target));
        }
    }
    
    private IEnumerator MoveCoroutine( float stopDistance, Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > stopDistance)
        {
            _owner.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        EventManager?.TriggerOnStopMoveEnemy(_owner.GetInstanceID());
    }
}
