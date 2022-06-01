using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemsPool : MonoBehaviour
{
    [SerializeField] private LevelManager level;

    private Dictionary<Rarity, ItemSO[]> globalItemsPool = new Dictionary<Rarity, ItemSO[]>();
    private Dictionary<Rarity, List<Item>> levelItemsPool = new Dictionary<Rarity, List<Item>>();

    private void Awake()
    {
        foreach (var rarity in Enum.GetValues(typeof(Rarity)))
        {
            Rarity tempRarity = (Rarity)rarity;

            levelItemsPool[tempRarity] = new List<Item>();

            LoadGlobalItems(tempRarity);
        }

        LoadLevelItems();
    }

    public Item[] GetItems(int amount, Rarity rarity)
    {
        int startingIndex;
        int resultAmount;
        
        startingIndex = levelItemsPool[rarity].Count - (amount + (levelItemsPool[rarity].Count - amount));
        resultAmount = startingIndex + amount > levelItemsPool[rarity].Count ? amount - ((startingIndex + amount) - levelItemsPool[rarity].Count) : amount;

        return levelItemsPool[rarity].GetRangeOut(startingIndex, resultAmount);
    }

    private void LoadGlobalItems(Rarity rarity) => globalItemsPool[rarity] = Resources.LoadAll<ItemSO>(@$"Items\{rarity}");

    private void LoadLevelItems()
    {
        foreach (var platform in level.Platfroms)
        {
            EncounterSO[] chestEncounters = platform.encounters.Where(encounter => encounter is ChestEncounterSO).ToArray();
            if (chestEncounters.Length < 0)
                continue;

            for (int i = 0; i < chestEncounters.Length; i++)
            {
                ChestEncounterSO realChestEncounter = (ChestEncounterSO)chestEncounters[i];
                ItemSO[] tempPool = globalItemsPool[realChestEncounter.Rarity];

                for (int j = 0; j < realChestEncounter.MaxItemsAmount; j++)
                {
                    levelItemsPool[realChestEncounter.Rarity].Add(tempPool[UnityEngine.Random.Range(0, tempPool.Length)].CreateInstance());
                }
            }
        }
    }
}
