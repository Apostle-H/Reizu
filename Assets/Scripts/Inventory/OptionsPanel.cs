using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ChestResolver chestResolver;

    [SerializeField] private Button takeBtn;
    [SerializeField] private Button equipUnequipBtn;

    private ItemSlot currentSlot;
    private ItemSlot targetSlot;

    public void ShowInventoryItemOptions(ItemSlot itemSlot)
    {
        if (itemSlot.item == null)
        {
            gameObject.SetActive(false);
            return;
        }

        currentSlot = itemSlot;

        equipUnequipBtnText.text = "Equip";
        equipUnequipBtn.onClick.RemoveAllListeners();
        equipUnequipBtn.onClick.AddListener(inventory.EquipItem);

        SetOptionsPanel();
    }

    public void ShowEquipItemOptions(int index)
    {
        if (equipmentSlots[(ItemType)index].item == null)
        {
            itemOptionsPanel.gameObject.SetActive(false);
            return;
        }

        currentSlot = equipmentSlots[(ItemType)index];

        equipUnequipBtnText.text = "Unequip";
        equipUnequipBtn.onClick.RemoveAllListeners();
        equipUnequipBtn.onClick.AddListener(UnequipItem);

        SetOptionsPanel();
    }

    public void SetOptionsPanel()
    {
        Vector3 targetPos = currentSlot.transform.GetChild(1).position;
        itemOptionsPanel.position = new Vector3(targetPos.x, targetPos.y);
        itemOptionsPanel.gameObject.SetActive(true);
    }
}
