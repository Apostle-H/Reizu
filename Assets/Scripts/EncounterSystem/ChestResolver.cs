using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestResolver : MonoBehaviour
{
    [SerializeField] private ItemsPool itemsPool;
    [SerializeField] private ItemMover itemMover;

    [SerializeField] private ItemSlot[] itemSlots;

    [SerializeField] private GameObject chestPanel;
    [SerializeField] private Button leave;

    public delegate void EndOpen();
    public event EndOpen onClose;

    private void OnEnable()
    {
        leave.onClick.AddListener(Leave);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            int tempI = i;
            itemSlots[i].itemButton.onClick.AddListener(() => itemMover.Choose(itemSlots[tempI]));
        }
    }

    private void OnDisable()
    {
        leave.onClick.RemoveListener(Leave);

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].itemButton.onClick.RemoveAllListeners();
        }
    }

    public void Open(int resultItemsAmount, Rarity rarity)
    {
        if (resultItemsAmount == 0)
            return;

        Item[] droppedItems = itemsPool.GetItems(resultItemsAmount, rarity);
        if (droppedItems.Length < 1)
        {
            Leave();
            return;
        }

        chestPanel.SetActive(true);
        for (int i = 0; i < droppedItems.Length; i++)
        {
            itemSlots[i].TrySetItem(droppedItems[i]);
        }
    }

    public void Leave()
    {
        chestPanel.SetActive(false);
        onClose?.Invoke();
    }
}
