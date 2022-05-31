using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private Explore explore;
    [SerializeField] private CombatResolver combatResolver;
    [SerializeField] private ChestResolver chestOpener;

    public delegate void Start();
    public event Start onStartEncounter;
    public delegate void End();
    public event End onEndEncounter;

    private Platform currentPlatform;
    private EncounterSO currentEncounter;
    private int encounterIndex = 0;

    public void StartEncounter(Platform platfrom)
    {
        if (platfrom.encounters == null || platfrom.encounters.Length < 1)
            return;

        currentPlatform = platfrom;
        currentEncounter = platfrom.encounters[encounterIndex];

        if (currentEncounter is CombatEncounterSO)
        {
            combatResolver.enabled = true;
            explore.enabled = false;

            combatResolver.SetUpCombat(platfrom, encounterIndex);
            combatResolver.onEndCombat += EndEncounter;
        }
        else if (currentEncounter is ChestEncounterSO)
        {
            chestOpener.enabled = true;
            explore.enabled = false;

            ChestEncounterSO tempEncounter = currentEncounter as ChestEncounterSO;

            chestOpener.onClose += EndEncounter;
            chestOpener.Open(tempEncounter.resultItemsAmount, tempEncounter.Rarity);
        }
        else if (currentEncounter is TameEncounterSO)
        {
            
        }
    }

    private void EndEncounter()
    {
        if (currentEncounter is CombatEncounterSO)
        {
            combatResolver.enabled = false;
            explore.enabled = true;
        }
        else if (currentEncounter is ChestEncounterSO)
        {
            chestOpener.enabled = false;
            explore.enabled = true;
        }

        encounterIndex++;
        if (encounterIndex >= currentPlatform.encounters.Length)
        {
            encounterIndex = 0;
            return;
        }

        StartEncounter(currentPlatform);
    }
}
