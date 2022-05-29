using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
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
}
