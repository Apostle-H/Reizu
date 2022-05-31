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
        int startingIndex;
        int resultAmount;
        switch (rarity)
        {
            case Rarity.common:
                startingIndex = commonPool.Count - (amount + (commonPool.Count - amount));
                resultAmount = startingIndex + amount > commonPool.Count ? amount - ((startingIndex + amount) - commonPool.Count) : amount;

                return commonPool.GetRangeOut(startingIndex, resultAmount);
            case Rarity.rare:
                startingIndex = rarePool.Count - (amount + (rarePool.Count - amount));
                resultAmount = startingIndex + amount > rarePool.Count ? amount - ((startingIndex + amount) - rarePool.Count) : amount;

                return rarePool.GetRangeOut(startingIndex, resultAmount);
            case Rarity.epic:
                startingIndex = epicPool.Count - (amount + (epicPool.Count - amount));
                resultAmount = startingIndex + amount > epicPool.Count ? amount - ((startingIndex + amount) - epicPool.Count) : amount;

                return epicPool.GetRangeOut(startingIndex, resultAmount);
            case Rarity.myth:
                startingIndex = mythPool.Count - (amount + (mythPool.Count - amount));
                resultAmount = startingIndex + amount > mythPool.Count ? amount - ((startingIndex + amount) - mythPool.Count) : amount;

                return mythPool.GetRangeOut(startingIndex, resultAmount);
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
