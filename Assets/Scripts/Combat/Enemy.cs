using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemySO stats;
    [HideInInspector] public int healthLeft;

    [SerializeField] private TextMeshProUGUI titleUI;
    [SerializeField] private Image healtBarUI;

    private void Awake()
    {
        titleUI.text = stats.title;
    }

    public Enemy(EnemySO stats)
    {
        this.stats = stats;
    }

    public bool TakeDamage(int damage)
    {
        healthLeft -= damage;
        healtBarUI.fillAmount = (float)healthLeft / (float)stats.health;

        if (healthLeft > 0)
            return false;

        gameObject.SetActive(false);
        return true;
    }
}
