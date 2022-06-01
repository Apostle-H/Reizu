using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item { get; private set; }

    [SerializeField] private Button corespondingButton;

    [SerializeField] private bool onlyTake;

    public bool OnlyTake { get { return onlyTake; } }

    public Button itemButton { get { return corespondingButton; } }

    public virtual bool TrySetItem(Item newItem)
    {
        item = newItem;

        ShowHideItem(item != null);
        return true;
    }

    public Item DeleteItem()
    {
        Item tempItem = item;
        item = null;

        ShowHideItem(item != null);

        return tempItem;
    }

    protected void ShowHideItem(bool show)
    {
        transform.GetChild(0).gameObject.SetActive(show);
    }
}
