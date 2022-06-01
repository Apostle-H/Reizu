using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAutomaticFighter", menuName = "AutomaticFighterSO")]
public class AutomaticFighterSO : EntitySO
{
    [SerializeField] private Rarity rarity;
    [SerializeField] private float attackSpeed;

    public Rarity Rarity { get { return rarity; } }
    public float AttackSpeed { get { return attackSpeed; } }
}
