using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item { get; private set; }

    [SerializeField] private Button corespondingButton;

    public Button button { get { return corespondingButton; } }

    public bool SetItem(Item newItem)
    {
        if (newItem == null)
            return false;

        if (item != null)
            return false;

        item = newItem;
        ShowHideItem(item != null);

        return true;
    }

    public Item SwapItem(Item newItem)
    {
        Item tempItem = item;
        item = newItem;

        ShowHideItem(item != null);

        return tempItem;
    }

    public Item DeleteItem()
    {
        Item tempItem = item;
        item = null;

        ShowHideItem(item != null);

        return tempItem;
    }

    private void ShowHideItem(bool show)
    {
        transform.GetChild(0).gameObject.SetActive(show);
    }
}
