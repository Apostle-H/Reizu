using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SwipeDetector swipeDetector;

    [SerializeField] private Transform player;

    private Platform currentPlatform;

    public delegate void CamePlatform(Platform platform);
    public event CamePlatform onCameOnPlatform;

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

        onCameOnPlatform?.Invoke(currentPlatform);
    }
}
