using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSummon", menuName = "SummonSO")]
public class SummonSO : ItemSO
{
    [SerializeField] private GameObject GFX;

    public GameObject graphics { get { return GFX; } }

    public Summon CreateInstance(AutomaticFighter fighter) => new Summon(title, rarity, type, riseStat, riseValue, sprite, fighter);
}
