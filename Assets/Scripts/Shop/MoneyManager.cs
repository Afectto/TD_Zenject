using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class MoneyManager :MonoBehaviour, IListener
{
    [Inject]public EventManager EventManager { get; }
    
    [SerializeField] private TextMeshProUGUI text;

    private float _incomePerSecond;
    
    public float CurrentMoney { get; private set; }

    [Inject]
    public void Initialize()
    {
        CurrentMoney = 5000;
        _incomePerSecond = 1000 / 60f;
        UpdateMoneyText();
        StartCoroutine(Income());
    }

    private IEnumerator Income()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            AddMoney(_incomePerSecond/2);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    public void OnEnable()
    {
        EventManager.OnBuyItemInShop += BuyItemInShop;
        EventManager.OnChangeMoney += UpdateMoneyText;
        EventManager.OnAddIncome += AddIncome;
        EventManager.OnRewardByEnemy += AddMoney;
    }

    private void BuyItemInShop(int price)
    {
        CurrentMoney -= price;
        EventManager?.TriggerOnChangeMoney();
    }

    private void AddMoney(float value)
    {
        CurrentMoney += value;
        EventManager?.TriggerOnChangeMoney();
    }

    private void UpdateMoneyText()
    {
        text.text = Mathf.FloorToInt(CurrentMoney).ToString();;
    }

    private void AddIncome(float value)
    {
        _incomePerSecond += value / 60;
    }

    public void OnDisable()
    {
        EventManager.OnBuyItemInShop -= BuyItemInShop;
        EventManager.OnChangeMoney -= UpdateMoneyText;
        EventManager.OnAddIncome -= AddIncome;
        EventManager.OnRewardByEnemy -= AddMoney;
    }
}