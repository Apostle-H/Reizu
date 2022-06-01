using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChestEncounter", menuName = "ChestEncounterSO")]
public class ChestEncounterSO : EncounterSO
{
    [SerializeField] private Rarity rarity;

    [SerializeField] [Range(1, 3)] private int minItemsAmount;
    [SerializeField] [Range(1, 3)] private int maxItemsAmount;

    public Rarity Rarity { get { return rarity; } }
    public int MinItemsAmount { get { return minItemsAmount; } }
    public int MaxItemsAmount { get { return maxItemsAmount; } }
    public int resultItemsAmount { get { return Random.Range(minItemsAmount, maxItemsAmount + 1); } }
}
