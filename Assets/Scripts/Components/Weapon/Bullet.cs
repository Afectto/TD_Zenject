using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    private Transform _target;
    private Vector3 _lastEnemyPosition;

    public event Action<Bullet> OnSetDamage; 
    
    public void Update()
    {
        if (!_target || !_target.gameObject.activeSelf)
        {
            _target = null;
            MoveBullet(_lastEnemyPosition);
            if (transform.position == _lastEnemyPosition)
            {
                DestroyBullet();
            }
            return;
        }
		
        MoveBullet(_target.position);
		
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
        _target = tar;
    }
    
    protected virtual void SetDamage()
    {
        OnSetDamage?.Invoke(this);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
