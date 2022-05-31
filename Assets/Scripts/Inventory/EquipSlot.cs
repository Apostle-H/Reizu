using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : ItemSlot
{
    [SerializeField] private ItemType fitingType;

    public override bool TrySetItem(Item newItem)
    {
        if (newItem == null || newItem.Type != fitingType )
        {
            return false;
        }

        return base.TrySetItem(newItem);
    }
}
