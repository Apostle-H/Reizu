using System.Collections;
using UnityEngine;
using TMPro;

public class Player : Entity
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private TextMeshProUGUI damageUI;
    [SerializeField] private TextMeshProUGUI defenceUI;

    protected override void OnEnable()
    {
        base.OnEnable();
        inventory.onEquip += OnEquip;
    }

    private void OnDisable()
    {
        inventory.onEquip -= OnEquip;
    }

    protected override void Awake()
    {
        base.Awake();

        healthLeft = health;
        healthUI.text = $"{healthLeft}/{health}";
        damageUI.text = $"dmg {damage}";
        defenceUI.text = $"def {defence}";
    }

    public override bool TakeDamage(int damage)
    {
        healthLeft -= damage - defence;
        healtBarUI.fillAmount = (float)healthLeft / (float)health;
        healthUI.text = $"{healthLeft}/{health}";

        if (healthLeft > 0)
            return false;

        gameObject.SetActive(false);
        return true;
    }

    public override void Attack(Entity enemy)
    {
        enemy?.TakeDamage(damage);
    }

    private void OnEquip(Item previousItem, Item newItem)
    {
        if (previousItem != null)
        {
            switch (previousItem.RiseStat)
            {
                case Stat.damage:
                    damage -= previousItem.RiseValue;
                    break;
                case Stat.defence:
                    defence -= previousItem.RiseValue;
                    break;
            }
        }

        if (newItem != null)
        {
            switch (newItem.RiseStat)
            {
                case Stat.damage:
                    damage += newItem.RiseValue;
                    break;
                case Stat.defence:
                    defence += newItem.RiseValue;
                    break;
            }
        }

        damageUI.text = $"dmg {damage}";
        defenceUI.text = $"def {defence}";
    }
}
