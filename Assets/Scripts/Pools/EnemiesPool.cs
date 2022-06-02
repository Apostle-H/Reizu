using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] LevelManager level;

    private Dictionary<Rarity, GameObject[]> globalPool = new Dictionary<Rarity, GameObject[]>();
    private Dictionary<Rarity, Queue<AutomaticFighter>> levelPool = new Dictionary<Rarity, Queue<AutomaticFighter>>();

    private void Awake()
    {
        foreach (var rarity in Enum.GetValues(typeof(Rarity)))
        {
            Rarity tempRarity = (Rarity)rarity;

            levelPool[tempRarity] = new Queue<AutomaticFighter>();

            LoadGlobalEnemies(tempRarity);
        }

        LoadLevelEnemies();
    }

    public AutomaticFighter GetEnemy(Rarity rarity) 
    {
        return levelPool[rarity]?.Dequeue();
    } 

    private void LoadGlobalEnemies(Rarity rarity) =>
        globalPool[rarity] = Resources.LoadAll<GameObject>(@$"Enemies\{rarity}");

    private void LoadLevelEnemies()
    {
        Rarity[] allEnemiesRarity = level.Platfroms.SelectMany(platfrom => platfrom.encounters).Where(encounter => encounter is CombatEncounterSO).
            SelectMany(enecounter => ((CombatEncounterSO)enecounter).enemiesRarity).ToArray();

        foreach (var rarity in allEnemiesRarity)
        {
            GameObject[] tempPool = globalPool[rarity].ToArray();
            GameObject fighterInstance = Instantiate(tempPool[UnityEngine.Random.Range(0, tempPool.Length)]);

            levelPool[rarity].Enqueue(fighterInstance.GetComponent<AutomaticFighter>());
        }
    }
}
