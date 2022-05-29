using UnityEngine;
using System.Linq;

public class CombatResolver : MonoBehaviour
{
    [SerializeField] private EnemiesPool enemiesPool;
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private Transform player;

    [SerializeField] private int damage;

    public delegate void EndCombat();
    public event EndCombat onEndCombat;

    private Enemy[] enemies = new Enemy[3];

    private void OnEnable()
    {
        swipeDetector.onSwipe += DealDamage;
    }

    private void OnDisable()
    {
        swipeDetector.onSwipe -= DealDamage;
    }

    public void SetUpCombat(Platform platform)
    {
        Enemy[] encounterEnemies = (platform.encounter as CombatEncounterSO).enemies;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (encounterEnemies[i] == null)
                continue;

            enemies[i] = enemiesPool.GetEnemy(encounterEnemies[i].stats.title).GetComponent<Enemy>();

            enemies[i].healthLeft = enemies[i].stats.health;
            enemies[i].gameObject.SetActive(true);
            platform.MoveToEnemy(enemies[i].gameObject.transform, i);
        }

        platform.MoveToCombat(player);
    }

    private void DealDamage(Side swipeSide)
    {
        foreach (var enemy in enemies)
        {
            enemy?.TakeDamage(damage);
        }

        if (enemies.All(enemy => enemy == null || enemy.healthLeft <= 0))
        {
            onEndCombat?.Invoke();
        }
    }
}
