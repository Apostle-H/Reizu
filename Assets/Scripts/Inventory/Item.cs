using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    [SerializeField] private string title;
    [SerializeField] private Rarity rarity;

    [SerializeField] private ItemType type;

    [SerializeField] private int riseValue;
    [SerializeField] private Stat riseStat;

    public string Title { get { return title; } }
    public ItemType Type { get { return type; } }
    public int RiseValue { get { return riseValue; } }
    public Stat RiseStat { get { return riseStat; } }

    public Item(string title, Rarity rarity, ItemType type, int riseValue, Stat riseStat)
    {
        this.title = title;
        this.rarity = rarity;
        this.type = type;
        this.riseValue = riseValue;
        this.riseStat = riseStat;
    }
}
