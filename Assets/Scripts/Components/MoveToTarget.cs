using System.Collections;
using UnityEngine;
using Zenject;

[DisallowMultipleComponent]
public class MoveToTarget : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}
    
    [SerializeField] private float speed;
    
    private GameObject _owner;
    private int _ownerID;
    
    private void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
    }

    public void OnEnable()
    {
        EventManager.MoveCommand += OnMoveToTarget;
        EventManager.OnChangeIceStatus += ChangeIceStatus;
    }

    public void OnDisable()
    {
        EventManager.MoveCommand -= OnMoveToTarget;
        EventManager.OnChangeIceStatus -= ChangeIceStatus;
    }

    private void ChangeIceStatus(int target, float value)
    {
        if (target == _ownerID)
        {
            speed += value;
        }
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
            yield return new WaitForSeconds(Random.Range(0, 0.01f));
        }
        EventManager?.TriggerOnStopMoveEnemy(_owner.GetInstanceID());
    }
}
