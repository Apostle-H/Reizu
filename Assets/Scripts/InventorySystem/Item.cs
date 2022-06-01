using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string title;
    [SerializeField] private Rarity rarity;

    [SerializeField] private ItemType type;
    [SerializeField] private Stat riseStat;

    [SerializeField] private int riseValue;

    public string Title { get { return title; } }
    public ItemType Type { get { return type; } }
    public Stat RiseStat { get { return riseStat; } }
    public int RiseValue { get { return riseValue; } }

    public Item(string title, Rarity rarity, ItemType type, Stat riseStat, int riseValue)
    {
        this.title = title;
        this.rarity = rarity;
        this.type = type;
        this.riseStat = riseStat;
        this.riseValue = riseValue;
    }
}
