using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Entity : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] protected int health;
    [SerializeField] protected int defence;
    [SerializeField] protected int damage;

    protected int healthLeft;

    public bool isDead = false;

    [SerializeField] protected TextMeshProUGUI titleUI;
    [SerializeField] protected Image healtBarUI;

    public string Title { get { return title; } }

    protected virtual void OnEnable()
    {
        healthLeft = health;
    }

    protected virtual void Awake()
    {
        titleUI.text = Title;
    }

    public virtual bool TakeDamage(int damage)
    {
        healthLeft -= damage - defence;
        healtBarUI.fillAmount = (float)healthLeft / (float)health;

        if (healthLeft > 0)
            return false;

        isDead = true;
        return true;
    }

    public virtual void Attack(Entity enemy) => enemy?.TakeDamage(damage);
}
