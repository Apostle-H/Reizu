using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Item[] items = new Item[8];

    private Item equipedWeapon;
    private Item equipedArmor;
    private Item equipedConsumable;

    [SerializeField] private GameObject panel;

    public int defenceBonus { get; private set; }
    public int damageBonus { get; private set; }

    public void Open() => panel.SetActive(true);
    public void Close() => panel.SetActive(false);

    public void AddItems(Item[] newItems)
    {
        for (int i = 0, j = 0; i < items.Length && j < newItems.Length; i++)
        {
            if (items[i] != null)
                continue;

            items[i] = newItems[j++];

            if (items[i] == null)
                continue;

            panel.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
    }
}
