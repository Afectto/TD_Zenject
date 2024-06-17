using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class StatusManager : MonoBehaviour, IListener
{
    [Inject] private ResourcesLoader _resourcesLoader;
    [Inject] public EventManager EventManager { get; }
    
    private DiContainer _container;
    
    private List<StatusEffect> _statusEffects;
    private List<IStatusEffect> _activeStatusEffects;

    [Inject]
    private void Initialized(DiContainer container)
    {
        _container = container;
        _activeStatusEffects = new List<IStatusEffect>();
        _statusEffects = _resourcesLoader.StatusEffects;
    }

    public void OnEnable()
    {
        if (EventManager != null)
        {
            EventManager.OnSetStatus += CreateActiveStatus;
            EventManager.OnRemoveActiveStatus += RemoveActiveStatus;
        }
    }

    public void OnDisable()
    {
        if (EventManager != null)
        {
            EventManager.OnSetStatus -= CreateActiveStatus;
            EventManager.OnRemoveActiveStatus -= RemoveActiveStatus;
        }
    }

    private void Update()
    {
        for (int i = 0; i < _activeStatusEffects.Count; i++)
        {
            _activeStatusEffects[i]?.UpdateStatus();
        }
    }

    private void CreateActiveStatus(StatusEffectType type, int targetId)
    {
        var status = _statusEffects.Find(stat =>
            stat.StatusEffectStatus == type );

        if (status.IsStackable)
        {
            CreateNewStatus(status, targetId);
        }
        else
        {
            var activeStatus = _activeStatusEffects.Find(stat => stat.GetTargetId() == targetId);
            if (activeStatus == null)
            {
                CreateNewStatus(status, targetId);
            }
            activeStatus?.RestartStatusEffect();
        }
    }

    private void CreateNewStatus(StatusEffect statusEffect, int targetId)
    {
        var newStatus = _container.Instantiate(statusEffect.GetType()) as IStatusEffect;
        newStatus.SetStatus(targetId, statusEffect);
        _activeStatusEffects.Add(newStatus);
    }

    private void RemoveActiveStatus(IStatusEffect statusEffect)
    {
        _activeStatusEffects.Remove(statusEffect);
    }
}