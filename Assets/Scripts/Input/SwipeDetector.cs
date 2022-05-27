using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxTime;

    public delegate void Swipe(Side swipeSide);
    public event Swipe OnSwipe;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;

    private void OnEnable()
    {
        inputHandler.OnStartTouch += SwipeStart;
        inputHandler.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputHandler.OnStartTouch += SwipeStart;
        inputHandler.OnEndTouch += SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;

        Detect();
    }

    private void Detect()
    {
        Vector2 resultSwipe = new Vector2(endPosition.x - startPosition.x, endPosition.y - startPosition.y);
        if (resultSwipe.magnitude < minDistance || endTime - startTime > maxTime)
            return;

        if (Mathf.Abs(resultSwipe.x) > Mathf.Abs(resultSwipe.y))
        {
            OnSwipe(resultSwipe.x < 0 ? Side.left : Side.right);
        }
        else
        {
            OnSwipe(resultSwipe.y < 0 ? Side.down : Side.up);
        }
    }
}
