using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Item item { get; private set; }

    [SerializeField] private Button corespondingButton;
    [SerializeField] private Image corespondingImage;
    [SerializeField] private Image rarityCorespondingImage;
    [SerializeField] private TextMeshProUGUI statText;

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
        corespondingImage.sprite = show ? item.Sprite : null;
        corespondingImage.enabled = show;

        if (show && item != null)
        {
            switch (item.Rarity)
            {
                case Rarity.common:
                    rarityCorespondingImage.color = RarityColors.common;
                    break;
                case Rarity.rare:
                    rarityCorespondingImage.color = RarityColors.rare;
                    break;
                case Rarity.epic:
                    rarityCorespondingImage.color = RarityColors.epic;
                    break;
                case Rarity.myth:
                    rarityCorespondingImage.color = RarityColors.myth;
                    break;
            }
        }

        if (item != null && item.Type == ItemType.consumable)
            statText.enabled = false;
        else
            statText.enabled = show;

        if (item == null || !show)
            return;

        statText.text = $"{item.RiseStat}: {item.RiseValue}";
    }
}
