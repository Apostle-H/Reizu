using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private Rarity rarity;

    [SerializeField] private ItemType type;

    [SerializeField] private int riseValue;
    [SerializeField] private Stat riseStat;

    [SerializeField] private int inStock;

    public string Title { get { return title; } }
    public ItemType Type { get { return type; } }
    public int RiseValue { get { return riseValue; } }
    public Stat RiseStat { get { return riseStat; } }
    public int InStock { get { return inStock; } }

    public Item CreateInstance()
    {
        return new Item(title, rarity, type, riseValue, riseStat);
    }
}
