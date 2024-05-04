using UnityEngine;
using UnityEngine.UI;
using Zenject;

[DisallowMultipleComponent]
public class Health : MonoBehaviour, IListener
{
    private GameObject _owner;
    private int _ownerID;

    [SerializeField] private Text textHealth;
    [SerializeField] private bool isShowText;
    [SerializeField] private Image fillHealthBar;
    [SerializeField] private float maxHealth;
    private float _currentHealth;

    [Inject] public EventManager EventManager { get; set; }

    private void Awake()
    {
        _owner = gameObject;
        _ownerID = _owner.GetInstanceID();
        _currentHealth = maxHealth;
        textHealth.gameObject.SetActive(isShowText);
        UpdateHealthLine();
    }

    public void OnEnable()
    {
        EventManager.OnSetDamage += EventManagerOnSetDamage;
    }

    private void EventManagerOnSetDamage(int target, float damage)
    {
        if(damage <= 0 ) return;
        
        if (_ownerID == target)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                EventManager?.TriggerOnDeath(_ownerID);
            }
            UpdateHealthLine();
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
    }
}
