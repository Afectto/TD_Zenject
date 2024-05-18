using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [SerializeField] private SlotData slotData;

    public SlotData SlotData => slotData;
    

}