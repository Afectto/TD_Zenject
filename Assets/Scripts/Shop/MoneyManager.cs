using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MoneyManager :MonoBehaviour, IListener
{
    [Inject]public EventManager EventManager { get; }
    [SerializeField] private TextMeshProUGUI text;

    public int CurrentMoney { get; private set; }

    [Inject]
    public void Initialize()
    {
        CurrentMoney = 5000;
        UpdateMoneyText();
    }
    
    private void BuyItemInShop(int price)
    {
        CurrentMoney -= price;
        EventManager?.TriggerOnChangeMoney();
    }

    public void AddMoney(int value)
    {
        CurrentMoney += value;
        EventManager?.TriggerOnChangeMoney();
    }

    private void UpdateMoneyText()
    {
        text.text = CurrentMoney.ToString();
    }

    public void OnEnable()
    {
        EventManager.OnBuyItemInShop += BuyItemInShop;
        EventManager.OnChangeMoney += UpdateMoneyText;
    }

    public void OnDisable()
    {
        EventManager.OnBuyItemInShop -= BuyItemInShop;
        EventManager.OnChangeMoney -= UpdateMoneyText;
    }
}