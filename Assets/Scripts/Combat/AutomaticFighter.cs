using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutomaticFighter : Entity
{
    [SerializeField] private Rarity rarity;
    [SerializeField] private float attackSpeed;

    public IEnumerator Fight(Entity enemy)
    {
        if (!isDead)
            Attack(enemy);
        
        yield return new WaitForSeconds(attackSpeed);

        if (!isDead)
            StartCoroutine(Fight(enemy));
    }
}
