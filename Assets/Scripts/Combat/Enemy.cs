using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : Entity
{
    public Rarity rarity;
    [SerializeField] protected float attackSpeed;

    public IEnumerator Fight(Entity enemy)
    {
        Attack(enemy);
        yield return new WaitForSeconds(attackSpeed);
        StartCoroutine(Fight(enemy));
    }
}
