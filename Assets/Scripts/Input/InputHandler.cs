using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerControls playerControls;

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    private void Awake() => playerControls = new PlayerControls();

    private void OnEnable() => playerControls.Enable();

    private void OnDisable() => playerControls.Disable();

    private void Start()
    {
        playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch == null)
            return;

        OnStartTouch(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch == null)
            return;

        OnEndTouch(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.time);
    }
}
