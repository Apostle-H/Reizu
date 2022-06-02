using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatResolver : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private EnemiesPool enemiesPool;
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private PlayerCombat player;

    public delegate void EndCombat();
    public event EndCombat onEndCombat;

    private AutomaticFighter[] enemies = new AutomaticFighter[3];

    private void OnEnable()
    {
        swipeDetector.onSwipe += DealDamage;
    }

    private void OnDisable()
    {
        swipeDetector.onSwipe -= DealDamage;
    }

    public void SetUpCombat(Platform platform, Rarity[] enemiesRarity)
    {
        for (int i = 0; i < enemiesRarity.Length; i++)
            enemies[i] = enemiesPool.GetEnemy(enemiesRarity[i]);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
                continue;

            enemies[i].gameObject.SetActive(true);
            enemies[i].transform.position = platform.CombatEnemiesAnchors[i].position;  

            StartCoroutine(enemies[i].Fight(player));
        }

        if (inventory.summons == null)
            return;

        for (int i = 0; i < inventory.summons.Length; i++)
        {
            if (inventory.summons[i] == null)
                continue;

            Summon realSummon = (Summon)inventory.summons[i];

            realSummon.fighter.gameObject.SetActive(true);
            realSummon.fighter.transform.position = platform.CombatSummonsAnchors[i].position;

            StartCoroutine(realSummon.fighter.Fight(enemies));
        }
    }

    private void DealDamage(Side swipeSide)
    {
        foreach (var enemy in enemies)
            player.Attack(enemy);

        if (enemies.All(enemy => (enemy == null || enemy.isDead)))
        {
            for (int i = 0; i < inventory.summons.Length; i++)
            {
                Summon realSummon = (Summon)inventory.summons[i];

                realSummon.fighter.gameObject.SetActive(true);
            }
            onEndCombat?.Invoke();
        }
    }
}
