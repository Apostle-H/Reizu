using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TameEncounterSO : EncounterSO
{
    [SerializeField] private Rarity rarity;

    [SerializeField] private int tameChance;

    public Rarity Rarity { get { return rarity; } }
    public int TameChance { get { return tameChance; } }
}
