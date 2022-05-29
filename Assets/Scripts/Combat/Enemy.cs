using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : Entity
{
    [SerializeField] private Rarity rarity;
    [SerializeField] private float attackSpeed;

    [SerializeField] private int minItems;
    [SerializeField] private int maxItems;

    public int DropItemsAmount { get { return Random.Range(minItems, maxItems); } }

    public IEnumerator Fight(Entity enemy)
    {
        if (!isDead)
            Attack(enemy);
        
        yield return new WaitForSeconds(attackSpeed);

        if (!isDead)
            StartCoroutine(Fight(enemy));
    }
}
