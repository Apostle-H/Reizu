using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ChestResolver chestResolver;

    private ItemSlot currentSlot;
    private ItemSlot targetSlot;

    public void Choose(ItemSlot itemSlot)
    {
        if (itemSlot.item == null)
        {
            return;
        }

        if (currentSlot == null)
        {
            currentSlot = itemSlot;
            return;
        }

        targetSlot = itemSlot;

        TrySwap();
    }

    private void TrySwap()
    {
        Item tempItem = targetSlot.item;

        if (targetSlot.TrySetItem(currentSlot.item))
        {
            currentSlot.TrySetItem(tempItem);
        }
    }
}
