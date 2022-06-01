using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : ItemSlot
{
    [SerializeField] private ItemType fitingType;

    public delegate void Equip(Item previousItem, Item newItem);
    public event Equip onEquip;

    public override bool TrySetItem(Item newItem)
    {
        if (newItem != null && newItem.Type != fitingType)
            return false;

        Item tempItem = item;
        bool result = base.TrySetItem(newItem);

        onEquip?.Invoke(tempItem, newItem);
        return result;
    }
}
