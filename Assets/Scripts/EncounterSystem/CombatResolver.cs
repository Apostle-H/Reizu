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

        foreach (var enemy in enemies)
        {
            if (enemy == null)
                continue;

            enemy.gameObject.SetActive(true);
            platform.MoveToEnemy(enemy.gameObject.transform);

            StartCoroutine(enemy.Fight(player));
        }

        platform.MoveToCombat(player.transform);

        if (inventory.summons == null)
            return;

        foreach (var summonAsItem in inventory.summons)
        {
            if (summonAsItem == null)
                continue;

            Summon realSummon = (Summon)summonAsItem;

            realSummon.fighter.gameObject.SetActive(true);
            platform.MoveToSummon(realSummon.fighter.transform);

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

                StopCoroutine(realSummon.fighter.Fight(enemies));
            }
            onEndCombat?.Invoke();
        }
    }
}
