using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemMover itemMover;
    [SerializeField] private PlayerCombat playerCombat;

    [SerializeField] private ItemsPool itemsPool;
    [SerializeField] private SummonsPool summonsPool;

    [SerializeField] private EquipSlot[] equipSlots;
    [SerializeField] private ItemSlot[] itemSlots;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform UI;
    [SerializeField] private GameObject losePanel;

    public delegate void Equip(Item previousItem, Item newItem);
    public event Equip onEquipChanged;

    public Item[] summons { get { return equipSlots.Select(slot => slot.item).Where(item => item == null ? false : item.Type == ItemType.summon).ToArray() ; }  }

    public Item[] equip { get { return equipSlots.Select(slot => slot.item).ToArray(); } }
    public Item[] items { get { return itemSlots.Select(slot => slot.item).ToArray(); } }

    private void OnEnable()
    {
        SaveLoad.onSaved += () => losePanel.SetActive(true);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            int tempI = i;
            itemSlots[i].itemButton.onClick.AddListener(() => itemMover.Choose(itemSlots[tempI]));
        }

        for (int i = 0; i < equipSlots.Length; i++)
        {
            int tempI = i;
            equipSlots[i].itemButton.onClick.AddListener(() => itemMover.Choose(equipSlots[tempI]));
            equipSlots[i].onEquip += (Item prevoiusItem, Item newItem) => onEquipChanged?.Invoke(prevoiusItem, newItem);
        }

        playerCombat.onDie += Save;
        SaveLoad.onSaved += () => losePanel.SetActive(true);

        Load();
    }

    private void OnDisable()
    {
        for (int i = 0; i < itemSlots.Length; i++)
            itemSlots[i].itemButton.onClick.RemoveAllListeners();

        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].itemButton.onClick.RemoveAllListeners();
            equipSlots[i].onEquip -= (Item prevoiusItem, Item newItem) => onEquipChanged?.Invoke(prevoiusItem, newItem);
        }
    }

    private void Save()
    {
        for (int i = 1; i < UI.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        SaveLoad.WriteJson(this);
    }

    private void Load()
    {
        Dictionary<string, (Rarity, string, bool)[]> savedItems = SaveLoad.ReadJson();

        if (savedItems == null)
            return;

        for (int i = 0; i < savedItems["equip"].Length; i++)
        {
            if (savedItems["equip"][i].Item2 == null)
                continue;

            if (!savedItems["equip"][i].Item3)
            {
                Item tempItem = itemsPool.LoadItem(savedItems["equip"][i].Item1, savedItems["equip"][i].Item2);
                equipSlots[i].TrySetItem(tempItem);
            }
            else
            {
                Summon tempSummon = summonsPool.LoadSummon(savedItems["equip"][i].Item1, savedItems["equip"][i].Item2);
                equipSlots[i].TrySetItem(tempSummon);
            }
        }
        for (int i = 0; i < savedItems["items"].Length; i++)
        {
            if (savedItems["items"][i].Item2 == null)
                continue;

            if (!savedItems["items"][i].Item3)
            {
                Item tempItem = itemsPool.LoadItem(savedItems["items"][i].Item1, savedItems["items"][i].Item2);
                itemSlots[i].TrySetItem(tempItem);
            }
            else
            {
                Summon tempSummon = summonsPool.LoadSummon(savedItems["items"][i].Item1, savedItems["items"][i].Item2);
                itemSlots[i].TrySetItem(tempSummon);
            }
        }
    }
}
