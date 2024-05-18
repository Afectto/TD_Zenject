using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SlotData : ISlotItem
{
    [SerializeField]private string name;
    [SerializeField]private Sprite skin;
    [SerializeField]private int price;
    public string Name => name;
    public Sprite Skin => skin;
    public int Price => price;
}

public interface ISlotItem
{    
    public string Name { get; }
    public Sprite Skin { get; }
    public int Price { get; }
}