using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMover : MonoBehaviour
{
    private ItemSlot currentSlot;
    private ItemSlot targetSlot;

    private bool isCurrentSlotChosen = false;

    public void Choose(ItemSlot itemSlot)
    {
        if (!isCurrentSlotChosen)
        {
            currentSlot = itemSlot;
            isCurrentSlotChosen = true;
            return;
        }

        if (itemSlot.OnlyTake)
            return;

        targetSlot = itemSlot;

        TrySwap();
    }

    private void TrySwap()
    {
        Item tempItem = targetSlot.item;

        if (targetSlot.TrySetItem(currentSlot.item))
            currentSlot.TrySetItem(tempItem);

        isCurrentSlotChosen = false;
    }
}
