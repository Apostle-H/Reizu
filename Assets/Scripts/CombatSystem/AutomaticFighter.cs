using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AutomaticFighter : MonoBehaviour, IEntity
{
    [SerializeField] public AutomaticFighterSO info;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Image healtBarUI;

    private int healthLeft;

    public bool isDead { get; private set; } = false;

    private void Awake()
    {
        title.text = info.Title;
        healthLeft = info.Health;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.DOScale(1f, .5f);
    }

    public void Attack(IEntity enemy) => enemy?.TakeDamage(info.Damage);

    public bool TakeDamage(int damage)
    {
        healthLeft -= damage - info.Defence;
        if (healtBarUI != null)
        {
            healtBarUI.fillAmount = (float)healthLeft / (float)info.Health;
        }

        if (healthLeft > 0)
            return false;

        transform.DOScale(0f, .3f).onComplete += () => gameObject.SetActive(false);
        isDead = true;
        return true;
    }

    public IEnumerator Fight(IEntity enemy)
    {
        yield return new WaitForSeconds(info.AttackSpeed);

        if (!isDead)
            Attack(enemy);

        if (!isDead)
            StartCoroutine(Fight(enemy));
    }

    public IEnumerator Fight(IEntity[] enemies)
    {
        yield return new WaitForSeconds(info.AttackSpeed);

        if (!isDead)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Attack(enemies[i]);
            }
        }

        if (!isDead && gameObject.activeSelf)
            StartCoroutine(Fight(enemies));
    }
}
