using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TameResolver : MonoBehaviour
{
    [SerializeField] private SummonsPool summonsPool;
    [SerializeField] private ItemMover itemMover;

    [SerializeField] private EquipSlot riseChanceSlot;
    [SerializeField] private EquipSlot summonSlot;

    [SerializeField] private GameObject tamePanel;
    [SerializeField] private Button tameBtn;
    [SerializeField] private Button leaveBtn;
    [SerializeField] private TextMeshProUGUI tameChanceUI;

    [Range(0f, 1f)] private float tameChance;
    private Rarity rarity;

    public delegate void EndTame();
    public event EndTame onEndTame;
    public delegate void Fail(int damage);
    public event Fail onFail;

    private bool tamed = false;

    private void OnEnable()
    {
        tamed = false;

        riseChanceSlot.onEquip += (Item previousItem, Item newItem) => ConsumeForChance();
        summonSlot.onEquip += (Item previousItem, Item newItem) => Leave(previousItem, newItem);

        riseChanceSlot.itemButton.onClick.AddListener(() => itemMover.Choose(riseChanceSlot));
        summonSlot.itemButton.onClick.AddListener(() => itemMover.Choose(summonSlot));

        tameBtn.onClick.AddListener(TryTame);
        leaveBtn.onClick.AddListener(JustLeave);
    }

    private void OnDisable()
    {
        riseChanceSlot.onEquip -= (Item previousItem, Item newItem) => ConsumeForChance();
        summonSlot.onEquip -= (Item previousItem, Item newItem) => Leave(previousItem, newItem);

        riseChanceSlot.itemButton.onClick.RemoveAllListeners();
        summonSlot.itemButton.onClick.RemoveAllListeners();

        tameBtn.onClick.RemoveAllListeners();
        leaveBtn.onClick.RemoveAllListeners();
        summonSlot.TrySetItem(null);
    }   

    public void StartTame(float tameChance, Rarity rarity)
    {
        this.tameChance = tameChance;
        this.rarity = rarity;

        tameChanceUI.text = $"{(int)(tameChance * 100)}%";
        tamePanel.SetActive(true);
    }

    private void TryTame()
    {
        if (tamed)
            return;

        bool tameResult = Random.Range(0f, 1f) <= tameChance;

        if (!tameResult)
        {
            onFail?.Invoke(10);
            return;
        }

        if (summonSlot.TrySetItem(summonsPool.GetSummon(rarity)))
        {
            tamed = true;
            return;
        }
    }

    private void ConsumeForChance()
    {
        if (riseChanceSlot.DeleteItem() == null)
            return;

        tameChance += 0.1f;
        tameChanceUI.text = $"{(int)(tameChance * 100)}%";
    }

    private void Leave(Item previousItem, Item newItem)
    {
        if (previousItem == null)
            return;

        JustLeave();
    }

    private void JustLeave()
    {
        tamePanel.SetActive(false);
        onEndTame?.Invoke();
    }
}
