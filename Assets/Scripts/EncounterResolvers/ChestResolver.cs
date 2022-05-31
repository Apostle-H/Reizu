using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestResolver : MonoBehaviour
{
    [SerializeField] private ItemsPool pool;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject openPanel;
    [SerializeField] private ItemSlot[] itemSlots;

    [SerializeField] private Button leave;

    public delegate void EndOpen();
    public event EndOpen onClose;

    private Item[] droppedItems;
    private List<Item> takenItems = new List<Item>();

    private void OnEnable()
    {
        leave.onClick.AddListener(Leave);
    }

    private void OnDisable()
    {
        leave.onClick.RemoveListener(Leave);
    }

    public void Open(int resultItemsAmount, Rarity rarity)
    {
        if (resultItemsAmount == 0)
            return;

        droppedItems = pool.GetItems(resultItemsAmount, rarity);
        if (droppedItems.Length < 1)
        {
            Leave();
            return;
        }

        openPanel.SetActive(true);
        for (int i = 0; i < droppedItems.Length; i++)
        {
            openPanel.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
    }

    public void TakeItem(int index)
    {
        takenItems.Add(droppedItems[index]);
        openPanel.transform.GetChild(0).GetChild(index).gameObject.SetActive(false);
    }

    public void Leave()
    {
        openPanel.SetActive(false);
        onClose?.Invoke();
    }
}
