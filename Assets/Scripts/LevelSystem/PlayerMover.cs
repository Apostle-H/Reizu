using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SwipeDetector swipeDetector;

    [SerializeField] private Transform player;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private Platform currentPlatform;

    public delegate void CamePlatform(Platform platform, Transform player);
    public event CamePlatform onCameOnPlatform;
    public delegate void StartMovement(Transform player);
    public event StartMovement onStartMovement;

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
        Move(Side.up);
    }

    private void Move(Side moveSide)
    {
        swipeDetector.onSwipe -= Move;

        Platform targetPlatform = null;
        switch (moveSide)
        {
            case Side.down:
                targetPlatform = levelManager.previous;
                player.eulerAngles = new Vector3(0, 180, 0);
                break;
            case Side.up:
                targetPlatform = levelManager.next;
                player.eulerAngles = new Vector3(0, 0, 0);
                break;
        }

        if (targetPlatform == null)
            return;

        currentPlatform = targetPlatform;

        onStartMovement?.Invoke(player);
        virtualCamera.Follow = currentPlatform.transform;
        player.DOMove(currentPlatform.MainAnchor.position, 2f).onComplete += () => { onCameOnPlatform?.Invoke(currentPlatform, player); if (enabled) { swipeDetector.onSwipe += Move; } };
    }
}
