using System;
using UnityEngine;

public class EventManager
{
    //MOVE
    public event Action<int, float, Vector3> MoveCommand;
    public void TriggerMoveCommand(int owner, float stopDistance, Vector3 target)
    {
        MoveCommand?.Invoke(owner, stopDistance, target);
    }

    public event Action<int> OnStopMoveEnemy;
    public void TriggerOnStopMoveEnemy(int owner)
    {
        OnStopMoveEnemy?.Invoke(owner);
    }

    //DAMAGE
    public event Action<int, float> OnSetDamage;
    public  void TriggerOnSetDamage(int target, float damage)
    {
        OnSetDamage?.Invoke(target, damage);
    }

    public event Action<int> OnDeath;
    public void TriggerOnDeath(int owner)
    {
        OnDeath?.Invoke(owner);
    }
    
    //GAME STATE
    public event Action OnTowerDestroy;
    public virtual void TriggerOnTowerDestroy()
    {
        OnTowerDestroy?.Invoke();
    }
}
