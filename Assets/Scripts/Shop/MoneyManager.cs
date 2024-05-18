using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MoneyManager :MonoBehaviour
{
    [Inject] private EventManager _eventManager;
    [SerializeField] private TextMeshProUGUI text;

    public int CurrentMoney { get; private set; }

    [Inject]
    public void Initialize()
    {
        _eventManager.OnBuyItemInShop += BuyItemInShop;
        _eventManager.OnChangeMoney += UpdateMoneyText;
        CurrentMoney = 5000;
        UpdateMoneyText();
    }
    
    private void BuyItemInShop(int price)
    {
        CurrentMoney -= price;
        _eventManager.TriggerOnChangeMoney();
    }

    public void AddMoney(int value)
    {
        CurrentMoney += value;
        _eventManager.TriggerOnChangeMoney();
    }

    private void UpdateMoneyText()
    {
        text.text = CurrentMoney.ToString();
    }

    private void OnDestroy()
    {
        _eventManager.OnBuyItemInShop -= BuyItemInShop;
        _eventManager.OnChangeMoney -= UpdateMoneyText;
    }
}