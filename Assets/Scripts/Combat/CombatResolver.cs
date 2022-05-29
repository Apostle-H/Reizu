using UnityEngine;
using System.Linq;

public class CombatResolver : MonoBehaviour
{
    [SerializeField] private EnemiesPool enemiesPool;
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private Player player;

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

            enemies[i] = enemiesPool.GetEnemy(encounterEnemies[i].Title).GetComponent<Enemy>();

            enemies[i].gameObject.SetActive(true);
            platform.MoveToEnemy(enemies[i].gameObject.transform, i);

            StartCoroutine(enemies[i].Fight(player));
        }

        platform.MoveToCombat(player.transform);
    }

    private void DealDamage(Side swipeSide)
    {
        foreach (var enemy in enemies)
        {
            player.Attack(enemy);
        }

        if (enemies.All(enemy => enemy.isDead))
        {
            onEndCombat?.Invoke();
        }
    }
}
