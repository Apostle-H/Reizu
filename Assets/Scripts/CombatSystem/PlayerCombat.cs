using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class PlayerCombat : MonoBehaviour, IEntity
{
    [SerializeField] private EntitySO info;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TameResolver tameResolver;

    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private TextMeshProUGUI damageUI;
    [SerializeField] private TextMeshProUGUI defenceUI;

    [SerializeField] private Image healtBarUI;

    public event Void onDie;
    public event Void onPunch;

    private int healthLeft;

    private bool isDead = false;

    private int itemDamageBonus;
    private int itemDefenceBonus;

    public int resultDamage { get { return info.Damage + itemDamageBonus; } }
    public int resultDefence { get { return info.Defence + itemDefenceBonus; } }

    private void OnEnable()
    {
        healthLeft = info.Health;
        inventory.onEquipChanged += OnEquip;
        tameResolver.onFail += (int damage) => TakeDamage(damage);
    }

    private void OnDisable()
    {
        inventory.onEquipChanged -= OnEquip;
        tameResolver.onFail -= (int damage) => TakeDamage(damage);
    }

    private void Awake()
    {
        healthLeft = info.Health;
        healthUI.text = $"{healthLeft}/{info.Health}";
        damageUI.text = $"dmg {resultDamage}";
        defenceUI.text = $"def {resultDefence}";
    }

    public bool TakeDamage(int damage)
    {
        healthLeft -= damage - resultDefence < 0 ? 0 : damage - resultDefence;
        healtBarUI.fillAmount = (float)healthLeft / (float)info.Health;
        healthUI.text = $"{healthLeft}/{info.Health}";

        if (healthLeft > 0)
            return false;

        onDie?.Invoke();
        return true;
    }

    public void Attack(IEntity enemy)
    {
        if (isDead)
            return;

        onPunch?.Invoke();
        enemy?.TakeDamage(resultDamage);
    }

    private void OnEquip(Item previousItem, Item newItem)
    {
        if (previousItem != null)
        {
            switch (previousItem.RiseStat)
            {
                case Stat.dmg:
                    itemDamageBonus -= previousItem.RiseValue;
                    break;
                case Stat.def:
                    itemDefenceBonus -= previousItem.RiseValue;
                    break;
            }
        }

        if (newItem != null)
        {
            switch (newItem.RiseStat)
            {
                case Stat.dmg:
                    itemDamageBonus += newItem.RiseValue;
                    break;
                case Stat.def:
                    itemDefenceBonus += newItem.RiseValue;
                    break;
            }
        }

        damageUI.text = $"dmg {resultDamage}";
        defenceUI.text = $"def {resultDefence}";
    }
}
