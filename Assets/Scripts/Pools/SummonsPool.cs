using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[DefaultExecutionOrder(-2)]
public class SummonsPool : MonoBehaviour
{
    [SerializeField] private LevelManager level;

    private Dictionary<Rarity, SummonSO[]> globalSummonPool = new Dictionary<Rarity, SummonSO[]>();
    private Dictionary<Rarity, Queue<Summon>> levelSummonPool = new Dictionary<Rarity, Queue<Summon>>();


    private void Awake()
    {
        foreach (var rarity in Enum.GetValues(typeof(Rarity)))
        {
            Rarity tempRarity = (Rarity)rarity;

            levelSummonPool[tempRarity] = new Queue<Summon>();

            LoadGlobalSummons(tempRarity);
        }

        LoadLevelSummons();
    }

    public Summon GetSummon(Rarity rarity) => levelSummonPool[rarity]?.Dequeue();

    public Summon LoadSummon(Rarity rarity, string title)
    {
        SummonSO[] tempPool = globalSummonPool[rarity];
        SummonSO tempSummon = tempPool.FirstOrDefault(summon => summon.Title == title);
        GameObject fighterInstance = Instantiate(tempSummon.graphics);

        return tempSummon.CreateInstance(fighterInstance.GetComponent<AutomaticFighter>());
    }

    private void LoadGlobalSummons(Rarity rarity) => globalSummonPool[rarity] = Resources.LoadAll<SummonSO>(@$"Summons\{rarity}");

    private void LoadLevelSummons()
    {
        Rarity[] allSummonsRarity = level.Platfroms.SelectMany(platfrom => platfrom.encounters).Where(encounter => encounter is TameEncounterSO).
            Select(enecounter => ((TameEncounterSO)enecounter).Rarity).ToArray();

        foreach (var rarity in allSummonsRarity)
        {
            SummonSO[] tempPool = globalSummonPool[rarity];
            SummonSO tempSummon = tempPool[UnityEngine.Random.Range(0, tempPool.Length)];
            GameObject fighterInstance = Instantiate(tempSummon.graphics);

            levelSummonPool[rarity].Enqueue(tempSummon.CreateInstance(fighterInstance.GetComponent<AutomaticFighter>()));
        }
    }
}
