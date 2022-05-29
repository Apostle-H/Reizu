using System.Collections;
using UnityEngine;
using TMPro;

public class Player : Entity
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI healthUI;

    protected override void Awake()
    {
        base.Awake();

        healthLeft = health;
        healthUI.text = $"{healthLeft}/{health}";
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
}
