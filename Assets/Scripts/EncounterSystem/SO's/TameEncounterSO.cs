using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTameEncounter", menuName = "TameEncounterSO")]
public class TameEncounterSO : EncounterSO
{
    [SerializeField] private Rarity rarity;

    [SerializeField] [Range(0f, 1f)] private float tameChance;

    public Rarity Rarity { get { return rarity; } }
    public float TameChance { get { return tameChance; } }
}
