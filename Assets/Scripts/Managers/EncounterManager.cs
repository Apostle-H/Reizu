using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private Explore explore;
    [SerializeField] private CombatResolver combatResolver;

    private EncounterSO currentEncounter;

    public void StartEncounter(Platform platfrom)
    {
        if (platfrom.encounter == null)
            return;

        currentEncounter = platfrom.encounter;

        if (currentEncounter is CombatEncounterSO)
        {
            combatResolver.enabled = true;
            explore.enabled = false;

            combatResolver.SetUpCombat(platfrom);
            combatResolver.onEndCombat += EndEncounter;
        }
    }

    private void EndEncounter()
    {
        if (currentEncounter is CombatEncounterSO)
        {
            combatResolver.enabled = false;
            explore.enabled = true;
        }
    }
}
