using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform mainAnchor;
    [SerializeField] private Transform combatAnchor;
    [SerializeField] private Transform[] combatEnemiesAnchors;
    [SerializeField] private Transform[] combatSummonsAnchors;


    public EncounterSO[] encounters;
    [HideInInspector] public bool encountersResolved;

    private int enemyPlaceIndex;
    private int summonPlaceIndex;

    public void MoveToMain(Transform movedObject)
    {
        movedObject.position = mainAnchor.position;
    }

    public void MoveToCombat(Transform moveObject)
    {
        moveObject.position = combatAnchor.position;
    }

    public void MoveToEnemy(Transform moveObject) => moveObject.position = combatEnemiesAnchors[enemyPlaceIndex++].position;


    public void MoveToSummon(Transform moveObject) => moveObject.position = combatSummonsAnchors[summonPlaceIndex++].position;
}
