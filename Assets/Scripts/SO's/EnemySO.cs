using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemy", menuName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    public string title;

    public int health;
    public float damage;
    public float attackSpeed;
}
