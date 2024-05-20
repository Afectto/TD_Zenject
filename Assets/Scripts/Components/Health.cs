using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[DisallowMultipleComponent]
public class Health : MonoBehaviour, IListener
{
    [Inject] public EventManager EventManager { get;}

    [SerializeField] private Text textHealth;
    [SerializeField] private bool isShowText;
    [SerializeField] private Image fillHealthBar;
    [SerializeField] private float maxHealth;
    
    private GameObject _owner;
    private int _ownerID;
    private float _currentHealth;
    private float _regenPerSecond;

    private float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            UpdateHealthLine();
        }
    }

    private void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
        CurrentHealth = maxHealth;
        _regenPerSecond = 0;
        textHealth.gameObject.SetActive(isShowText);
        UpdateHealthLine();
    }

    public void OnEnable()
    {
        EventManager.OnSetDamage += EventManagerOnSetDamage;
        EventManager.OnAddRegenToTower += AddRegenToTower;
        EventManager.OnAddHealthToTower += AddMaxHealth;
    }

    private void AddRegenToTower(int owner, float value)
    {
        if (_ownerID == owner)
        {
            if (_regenPerSecond == 0)
            {
                StartCoroutine(Regen());
            }
            _regenPerSecond += value;
        }
    }

    private IEnumerator Regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            AddHealth(_regenPerSecond / 4);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void EventManagerOnSetDamage(int target, float damage)
    {
        if(damage <= 0 ) return;
        
        if (_ownerID == target)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                EventManager?.TriggerOnDeath(_ownerID);
            }
        }
    }

    private void AddHealth(float value)
    {
        CurrentHealth += value;
        if (CurrentHealth >= maxHealth)
            CurrentHealth = maxHealth;
    }

    private void AddMaxHealth(int owner, float value)
    {
        if (_ownerID == owner)
        {
            maxHealth += value;
            CurrentHealth += value;
        }
    }
    
    private void UpdateHealthLine()
    {
        textHealth.text = Mathf.FloorToInt(_currentHealth) + " / " + Mathf.FloorToInt(maxHealth);
        fillHealthBar.fillAmount = _currentHealth / maxHealth;
    }
    
    public void OnDisable()
    {
        EventManager.OnSetDamage -= EventManagerOnSetDamage;
        EventManager.OnAddRegenToTower -= AddRegenToTower;
        EventManager.OnAddHealthToTower -= AddMaxHealth;
    }
}
