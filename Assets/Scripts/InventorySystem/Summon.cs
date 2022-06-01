using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Item
{
    [SerializeField] private AutomaticFighter automaticFighter;

    public AutomaticFighter fighter { get { return automaticFighter; } }

    public Summon(string title, Rarity rarity, ItemType type, Stat riseStat, int riseValue, AutomaticFighter fighter) : base(title, rarity, type, riseStat, riseValue)
    {
        automaticFighter = fighter;
    }
}
