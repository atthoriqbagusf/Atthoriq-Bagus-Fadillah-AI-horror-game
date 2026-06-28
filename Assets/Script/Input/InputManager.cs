using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static GameInputAction;

public class InputManager : MonoBehaviour, IPlayerActions
{
    public UnityEvent<Vector2> OnMoveInput;
    public UnityEvent<bool> OnSprintInput;
    private GameInputAction _inputAction;
    public UnityEvent OnFlashlightInput;
    public UnityEvent OnInteractInput = new UnityEvent();

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractInput?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSprintInput?.Invoke(true);
        }
        if (context.canceled)
        {
            OnSprintInput?.Invoke(false);
        }
    }

    private void OnDisable()
    {
        _inputAction?.Disable();
    }

    void Awake()
    {
        _inputAction = new GameInputAction();
        _inputAction.Enable();
        _inputAction.Player.Enable();
        _inputAction.Player.SetCallbacks(this);
    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnFlashlightInput?.Invoke();
        }
    }
}
