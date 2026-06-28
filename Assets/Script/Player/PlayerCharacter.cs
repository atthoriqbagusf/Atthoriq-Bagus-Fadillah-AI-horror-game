using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    public PlayerCharacterMovement _movement;
    [SerializeField]
    private PlayerCharacterStamina _stamina;
    [SerializeField]
    private InventoryManager _inventory;
    [SerializeField]
    private CameraManager _camera;
    [SerializeField]
    private InteractDetector _interactDetector;
    [SerializeField]
    private InputManager _input;
    [SerializeField]
    private Flashlight _flashlight;

    public PlayerCharacterMovement Movement => _movement;
    public PlayerCharacterStamina Stamina => _stamina;
    public InventoryManager Inventory => _inventory;
    public CameraManager Camera => _camera;
    public InteractDetector InteractDetector => _interactDetector;
    public InputManager Input => _input; 
    public Flashlight Flashlight => _flashlight;

    public UnityEvent OnDeath;

    public bool IsHiding {get; private set;}

    public void SetIsHiding(bool isHiding)
    {
        IsHiding = isHiding;
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

