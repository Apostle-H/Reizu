using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    [SerializeField] private LevelManager level;

    [SerializeField] private Transform player;

    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private Platform currentPlatform;

    private void OnEnable()
    {
        swipeDetector.OnSwipe += Move;
    }

    private void OnDisable()
    {
        swipeDetector.OnSwipe -= Move;
    }

    private void Start()
    {
        currentPlatform = level.Start();

        currentPlatform.MoveTo(player);
    }

    private void Move(Side moveSide)
    {
        Platform targetPlatform = null;
        switch (moveSide)
        {
            case Side.down:
                targetPlatform = level.previous;
                break;
            case Side.up:
                targetPlatform = level.next;
                break;
        }

        if (targetPlatform == null)
            return;

        currentPlatform = targetPlatform;
        currentPlatform.MoveTo(player);
    }
}
