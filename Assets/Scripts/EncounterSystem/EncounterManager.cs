using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private PlayerMover explore;
    [SerializeField] private CombatResolver combatResolver;
    [SerializeField] private ChestResolver chestResolver;
    [SerializeField] private TameResolver tameResolver;

    private Platform currentPlatform;
    private EncounterSO currentEncounter;
    private int encounterIndex = 0;

    private void Awake()
    {
        explore.onCameOnPlatform += (platfrom, player) => FaceEncounter(platfrom);

        combatResolver.onEndCombat += FinishEncounter;
        chestResolver.onClose += FinishEncounter;
        tameResolver.onEndTame += FinishEncounter;
    }

    //private void OnDisable()
    //{
    //    explore.onCameOnPlatform -= (platfrom, player) => FaceEncounter(platfrom);

    //    combatResolver.onEndCombat -= FinishEncounter;
    //    chestResolver.onClose -= FinishEncounter;
    //    tameResolver.onEndTame -= FinishEncounter;
    //}

    public void FaceEncounter(Platform platfrom)
    {
        if (platfrom.encounters == null || platfrom.encounters.Length < 1 || platfrom.encountersResolved)
            return;

        currentPlatform = platfrom;
        currentEncounter = platfrom.encounters[encounterIndex];

        if (currentEncounter is CombatEncounterSO)
        {
            combatResolver.enabled = true;
            explore.enabled = false;

            CombatEncounterSO tempEncounter = (CombatEncounterSO)currentEncounter;

            combatResolver.SetUpCombat(platfrom, tempEncounter.enemiesRarity);
        }
        else if (currentEncounter is ChestEncounterSO)
        {
            chestResolver.enabled = true;
            explore.enabled = false;

            ChestEncounterSO tempEncounter = (ChestEncounterSO)currentEncounter;

            chestResolver.Open(tempEncounter.resultItemsAmount, tempEncounter.Rarity);
        }
        else if (currentEncounter as TameEncounterSO)
        {
            explore.enabled = false;
            tameResolver.enabled = true;

            TameEncounterSO tempEncounter = (TameEncounterSO)currentEncounter;

            tameResolver.StartTame(tempEncounter.TameChance, tempEncounter.Rarity);
        }
    }

    private void FinishEncounter()
    {

        if (currentEncounter is CombatEncounterSO)
        {
            combatResolver.enabled = false;
            explore.enabled = true;
        }
        else if (currentEncounter is ChestEncounterSO)
        {
            chestResolver.enabled = false;
            explore.enabled = true;
        }
        else if (currentEncounter is TameEncounterSO)
        {
            tameResolver.enabled = false;
            explore.enabled = true;
        }

        encounterIndex++;
        if (encounterIndex >= currentPlatform.encounters.Length || currentPlatform.encountersResolved)
        {
            encounterIndex = 0;
            currentPlatform.encountersResolved = true;
            return;
        }

        FaceEncounter(currentPlatform);
    }
}
