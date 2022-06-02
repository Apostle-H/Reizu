using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform mainAnchor;
    [SerializeField] private Transform[] combatEnemiesAnchors;
    [SerializeField] private Transform[] combatSummonsAnchors;

    public EncounterSO[] encounters;
    [HideInInspector] public bool encountersResolved;

    public Transform MainAnchor { get { return mainAnchor; } }
    public Transform[] CombatEnemiesAnchors { get { return combatEnemiesAnchors; } }
    public Transform[] CombatSummonsAnchors { get { return combatSummonsAnchors; } }
}
