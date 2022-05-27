using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    public delegate void StartTouch(Vector2 direction, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 direction, float time);
    public event EndTouch OnEndTouch;

    private void Awake() => playerControls = new PlayerControls();

    private void OnEnable() => playerControls.Enable();

    private void OnDisable() => playerControls.Disable();

    private void Start()
    {
        playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context) =>
        OnStartTouch?.Invoke(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.startTime);

    private void EndTouchPrimary(InputAction.CallbackContext context) =>
        OnEndTouch?.Invoke(playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(), (float)context.time);

    public Vector2 PrimaryPosition() => playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
}
