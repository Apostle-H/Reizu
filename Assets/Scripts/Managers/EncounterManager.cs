using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private Explorer mover;

    private Encounter currentEncounter;

    public void StartEncounter(Encounter encounter)
    {
        currentEncounter = encounter;

        currentEncounter.Appear();
        currentEncounter.OnEndEncounter += EndEncounter;
        if (currentEncounter is CombatEncounter)
        {
            mover.enabled = false;
        }
    }

    private void EndEncounter()
    {
        if (currentEncounter is CombatEncounter)
        {
            mover.enabled = true;
        }
    }
}
