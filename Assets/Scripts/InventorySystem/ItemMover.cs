using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMover : MonoBehaviour
{
    private ItemSlot currentSlot;
    private ItemSlot targetSlot;

    public void Choose(ItemSlot itemSlot)
    {
        if (currentSlot == null && itemSlot.item != null)
        {
            currentSlot = itemSlot;
            return;
        }

        if (itemSlot.OnlyTake || currentSlot == null)
            return;

        targetSlot = itemSlot;

        TrySwap();
    }

    private void TrySwap()
    {
        Item tempItem = targetSlot.item;

        if (targetSlot.TrySetItem(currentSlot.item))
            if (!currentSlot.TrySetItem(tempItem))
            {
                currentSlot.TrySetItem(targetSlot.item);
                targetSlot.TrySetItem(tempItem);
            }
            

        currentSlot = null;
        targetSlot = null;
    }
}
