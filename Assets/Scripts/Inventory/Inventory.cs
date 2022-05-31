using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private EncounterManager encounterManager;

    [SerializeField] private ItemSlot[] inventorySlots;
    [SerializeField] private ItemSlot[] equipForFill;

    private Dictionary<ItemType, ItemSlot> equipmentSlots = new Dictionary<ItemType, ItemSlot>();

    [SerializeField] private GameObject inventoryPanel;

    [SerializeField] private Transform itemOptionsPanel;
    [SerializeField] private Button equipUnequipBtn;
    [SerializeField] private TextMeshProUGUI equipUnequipBtnText;
    private ItemSlot currentSlot;

    public delegate void Equip(Item previousItem, Item newItem);
    public event Equip onEquip;

    private void Awake()
    {
        equipmentSlots.Add(ItemType.weapon, equipForFill[0]);
        equipmentSlots.Add(ItemType.armor, equipForFill[1]);
        equipmentSlots.Add(ItemType.consumable, equipForFill[2]);
        equipmentSlots.Add(ItemType.summon, equipForFill[3]);
    }

    private void OnEnable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            int tempI = i;
            inventorySlots[i].button.onClick.AddListener(() => ShowInventoryItemOptions(tempI));
        }

        for (int i = 0; i < equipForFill.Length; i++)
        {
            int tempI = i;
            equipForFill[i].button.onClick.AddListener(() => ShowEquipItemOptions(tempI));
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].button.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < equipForFill.Length; i++)
        {
            equipForFill[i].button.onClick.AddListener(() => ShowEquipItemOptions(i));
        }
    }

    public void OpenClose()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void OpenClose(bool active)
    {
        inventoryPanel.SetActive(active);
    }

    //public bool AddItems(Item[] newItems)
    //{
    //    for (int i = 0, j = 0; i < inventorySlots.Length && j < newItems.Length; i++)
    //    {
    //        if (i >= inventorySlots.Length - 1 && j < newItems.Length)
    //        {
    //            return false;
    //        }

    //        if (!inventorySlots[i].SetItem(newItems[j++]))
    //        {
    //            i--;
    //            continue;
    //        }
    //    }

    //    return true;
    //}

    public void EquipItem()
    {
        ItemType tempType = currentSlot.item.Type;

        onEquip?.Invoke(equipmentSlots[tempType].item, currentSlot.item);

        currentSlot.SetItem(equipmentSlots[tempType].SwapItem(currentSlot.DeleteItem()));
    }

    public void UnequipItem()
    {
        Item tempItem = currentSlot.DeleteItem();

        if (AddItems(new Item[] { tempItem }))
        {
            onEquip?.Invoke(tempItem, null);
            return;
        }

        currentSlot.SetItem(tempItem);
    }

    public void SellItem() => currentSlot.DeleteItem();
}
