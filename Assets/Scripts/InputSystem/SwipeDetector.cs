using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] private InputManager inputHandler;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxTime;

    public delegate void Swipe(Side swipeSide);
    public event Swipe onSwipe;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;

    private void OnEnable()
    {
        inputHandler.onStartTouch += SwipeStart;
        inputHandler.onEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputHandler.onStartTouch += SwipeStart;
        inputHandler.onEndTouch += SwipeEnd;
    }

    private void SwipeStart(Vector2 direction, float time)
    {
        startPosition = direction;
        startTime = time;
    }

    private void SwipeEnd(Vector2 direction, float time)
    {
        endPosition = direction;
        endTime = time;

        Detect();
    }

    private void Detect()
    {
        Vector2 resultSwipe = endPosition - startPosition;
        
        if (resultSwipe.magnitude < minDistance || endTime - startTime > maxTime)
            return;

        if (Mathf.Abs(resultSwipe.x) > Mathf.Abs(resultSwipe.y))
        {
            onSwipe?.Invoke(resultSwipe.x < 0 ? Side.left : Side.right);
        }
        else
        {
            onSwipe?.Invoke(resultSwipe.y < 0 ? Side.down : Side.up);
        }
    }
}
