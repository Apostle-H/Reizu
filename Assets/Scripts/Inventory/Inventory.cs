using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private EncounterManager encounterManager;
    [SerializeField] private ItemMover itemMover;

    [SerializeField] private ItemSlot[] equip;
    [SerializeField] private ItemSlot[] inventorySlots;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button openCloseBtn;

    public delegate void Equip(Item previousItem, Item newItem);
    public event Equip onEquip;

    private void OnEnable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].button.onClick.AddListener(() => itemMover.Choose(inventorySlots[i]));
        }

        for (int i = 0; i < equip.Length; i++)
        {
            equip[i].button.onClick.AddListener(() => itemMover.Choose(equip[i]));
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].button.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < equip.Length; i++)
        {
            equip[i].button.onClick.RemoveAllListeners();
        }
    }

    public void OpenClose() => inventoryPanel.SetActive(!inventoryPanel.activeSelf);
}
