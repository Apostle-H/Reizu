using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPool : MonoBehaviour
{
    private List<Item> commonPool = new List<Item>();
    private List<Item> rarePool = new List<Item>();
    private List<Item> epicPool = new List<Item>();
    private List<Item> mythPool = new List<Item>();

    private void Awake()
    {
        LoadItems();
        MixItems();
    }

    public Item[] GetItems(int amount, Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.common:
                return commonPool.GetRange(commonPool.Count - amount, amount).ToArray();
            case Rarity.rare:
                return rarePool.GetRange(commonPool.Count - amount, amount).ToArray();
            case Rarity.epic:
                return epicPool.GetRange(commonPool.Count - amount, amount).ToArray();
            case Rarity.myth:
                return mythPool.GetRange(commonPool.Count - amount, amount).ToArray();
        }

        return null;
    }

    private void LoadItems()
    {
        ItemSO[] itemsBlueprints = Resources.LoadAll<ItemSO>(@"Items\Common");
        
        for (int i = 0; i < itemsBlueprints.Length; i++)
        {
            for (int j = 0; j < itemsBlueprints[i].InStock; j++)
            {
                commonPool.Add(itemsBlueprints[i].CreateInstance());
            }
        }

        itemsBlueprints = Resources.LoadAll<ItemSO>(@"Items\Rare");

        for (int i = 0; i < itemsBlueprints.Length; i++)
        {
            for (int j = 0; j < itemsBlueprints[i].InStock; j++)
            {
                rarePool.Add(itemsBlueprints[i].CreateInstance());
            }
        }

        itemsBlueprints = Resources.LoadAll<ItemSO>(@"Items\Epic");

        for (int i = 0; i < itemsBlueprints.Length; i++)
        {
            for (int j = 0; j < itemsBlueprints[i].InStock; j++)
            {
                epicPool.Add(itemsBlueprints[i].CreateInstance());
            }
        }
        
        itemsBlueprints = Resources.LoadAll<ItemSO>(@"Items\Myth");

        for (int i = 0; i < itemsBlueprints.Length; i++)
        {
            for (int j = 0; j < itemsBlueprints[i].InStock; j++)
            {
                mythPool.Add(itemsBlueprints[i].CreateInstance());
            }
        }
    }

    private void MixItems()
    {
        commonPool.Shuffle();
        rarePool.Shuffle();
        epicPool.Shuffle();
        mythPool.Shuffle();
    }
}
