using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private ItemMover itemMover;

    [SerializeField] private EquipSlot[] equipSlots;
    [SerializeField] private ItemSlot[] itemSlots;

    [SerializeField] private GameObject inventoryPanel;

    public delegate void Equip(Item previousItem, Item newItem);
    public event Equip onEquipChanged;

    public Item[] summons { get { return equipSlots.Select(slot => slot.item).Where(item => item == null ? false : item.Type == ItemType.summon).ToArray() ; }  }

    private void OnEnable()
    {
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
}
