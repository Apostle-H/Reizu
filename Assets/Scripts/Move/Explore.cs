using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private EncounterManager encounterManager;

    [SerializeField] private Transform player;

    [SerializeField] private Platform currentPlatform;

    private void OnEnable()
    {
        swipeDetector.onSwipe += Move;
    }

    private void OnDisable()
    {
        swipeDetector.onSwipe -= Move;
    }

    private void Start()
    {
        currentPlatform = levelManager.Start();

        currentPlatform.MoveToMain(player);
    }

    private void Move(Side moveSide)
    {
        Platform targetPlatform = null;
        switch (moveSide)
        {
            case Side.down:
                targetPlatform = levelManager.previous;
                break;
            case Side.up:
                targetPlatform = levelManager.next;
                break;
        }

        if (targetPlatform == null)
            return;

        currentPlatform = targetPlatform;
        currentPlatform.MoveToMain(player);

        if (currentPlatform.encounterResolved)
            return;

        encounterManager.StartEncounter(currentPlatform);
        currentPlatform.encounterResolved = true;
    }
}
