using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntity", menuName = "EntitySO")]
public class EntitySO : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private int health;
    [SerializeField] private int defence;
    [SerializeField] private int damage;

    public string Title { get { return title; } }
    public int Health { get { return health; } }
    public int Defence { get { return defence; } }
    public int Damage { get { return damage; } }
}
