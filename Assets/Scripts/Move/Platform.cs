using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform mainAnchor;
    [SerializeField] private Transform combatAnchor;
    [SerializeField] private Transform[] combatEnemiesAnchors = new Transform[3];

    public EncounterSO encounter;
    [HideInInspector] public bool encounterResolved;

    public void MoveToMain(Transform movedObject)
    {
        movedObject.position = mainAnchor.position;
    }

    public void MoveToCombat(Transform moveObject)
    {
        moveObject.position = combatAnchor.position;
    }

    public void MoveToEnemy(Transform moveObject, int index)
    {
        moveObject.position = combatEnemiesAnchors[index].position;
    }
}
