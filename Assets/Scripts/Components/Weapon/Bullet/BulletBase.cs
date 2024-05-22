using System;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour , IBullet
{
    private float _speed;
    private Vector3 _lastEnemyPosition;
    
    protected Transform Target;
    
    protected event Action onEndMoveBulletToDeadTarget;
    public event Action<IBullet, int> OnSetDamage;
    public event Action<IBullet> onBulletDestroy;

    public void Update()
    {
        if (!Target || !Target.gameObject.activeSelf)
        {
            Target = null;
            MoveBullet(_lastEnemyPosition);
            if (transform.position == _lastEnemyPosition)
            {
                onEndMoveBulletToDeadTarget?.Invoke();
                DestroyBullet();
            }
            return;
        }
		
        MoveBullet(Target.position);
		
        if (transform.position == _lastEnemyPosition)
        {
            SetDamage();
        }
    }

    private void MoveBullet(Vector3 targetPosition)
    {
        var transformPosition = transform.position;

        transformPosition = Vector3.MoveTowards(transformPosition, targetPosition, Time.deltaTime * _speed);
        transform.position = transformPosition;
        _lastEnemyPosition = targetPosition;

        var difference = targetPosition - transformPosition;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void OnSetSpeedAndTarget(float speed, Transform tar)
    {
        _speed = speed;
        Target = tar;
    }

    protected virtual void SetDamage()
    {
        InvokeSetDamage(Target.gameObject.GetInstanceID());
        DestroyBullet();
    }

    protected void InvokeSetDamage(int targetInstanceId)
    {
        OnSetDamage?.Invoke(this, targetInstanceId);
    }
    
    protected void DestroyBullet()
    {
        onBulletDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}