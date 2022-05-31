using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCombatEncounter", menuName = "CombatEncounterSO")]
public class CombatEncounterSO : EncounterSO
{
    public AutomaticFighter[] enemies = new AutomaticFighter[3];
}
