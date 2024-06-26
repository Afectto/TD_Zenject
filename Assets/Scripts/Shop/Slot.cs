using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Slot : MonoBehaviour
{
    [Inject] private EventManager _eventManager;
    [Inject] private MoneyManager _moneyManager;
    
    [SerializeField] private Image itemSkin;
    [SerializeField] private TextMeshProUGUI text;
    
    private string _nameItem;
    private int _price;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        
    }

    private void OnClick()
    {
        if (_moneyManager.CurrentMoney >= _price)
        {
            _eventManager?.TriggerOnBuyItemInShop(_price);
            _eventManager?.TriggerOnNeedCreatePurchasedItem(_nameItem);
            ChangeActiveSlotItem(false);
        }
    }

    private void ChangeActiveSlotItem(bool value)
    {
        itemSkin.gameObject.SetActive(value);
        text.gameObject.SetActive(value);
    }
    
    public void SetSlotData(ISlotItem slotData)
    {
        ChangeActiveSlotItem(true);
        
        _nameItem = slotData.Name;
        itemSkin.sprite = slotData.Skin;
        text.text = slotData.Price.ToString();
        _price = slotData.Price;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}