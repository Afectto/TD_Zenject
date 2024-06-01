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
    }

    public void OnEnable()
    {
        AddEventIsEnemy();
        EventManager.OnAddRegenToTower += AddRegenToTower;
        EventManager.OnAddHealthToTower += AddMaxHealth;
    }

    #region EnemyEvent
    private void AddEventIsEnemy()
    {        
        var isEnemy = GetComponent<Enemy>();
        if (isEnemy)
        {
            
            EventManager.OnSetDamageToEnemy += SetDamageEnemy;
        }
        else
        {
            EventManager.OnSetDamage += SetDamage;
        }
    }

    private void RemoveEventIsEnemy()
    {
        var isEnemy = GetComponent<Enemy>();
        if (isEnemy)
        {
            
            EventManager.OnSetDamageToEnemy -= SetDamageEnemy;
        }
        else
        {
            EventManager.OnSetDamage -= SetDamage;
        }
    }
    #endregion
    
    private void SetDamageEnemy(int owner, float damage, WeaponDamageType weaponDamageType)
    {
        if(damage <= 0 ) return;

        if (_ownerID == owner)
        {
            var armorBehaviour = GetComponent<ArmorBehaviour>();
            var damageReducedByArmor = armorBehaviour.GetDamageReducedByArmor(damage, weaponDamageType);
            SetDamage(damageReducedByArmor);
        }
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

    private void SetDamage(int target, float damage)
    {
        if(damage <= 0 ) return;
        
        if (_ownerID == target)
        {
            SetDamage(damage);
        }
    }

    private void SetDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            EventManager?.TriggerOnDeath(_ownerID);
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
        RemoveEventIsEnemy();
        EventManager.OnAddRegenToTower -= AddRegenToTower;
        EventManager.OnAddHealthToTower -= AddMaxHealth;
    }
    
    
}
