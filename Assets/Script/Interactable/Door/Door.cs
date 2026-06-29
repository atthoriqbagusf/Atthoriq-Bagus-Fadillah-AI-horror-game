using UnityEngine;
using UnityEngine.Events;

public abstract class Door : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected Transform _doorTransform;
    [SerializeField]
    protected float _duration = 1f;
    [SerializeField]
    protected bool _isLocked;
    [SerializeField]
    protected string _keyID;

    protected bool _isOpen;
    protected bool _isAnimating;
    protected Coroutine _animatingDoorCoroutine;

    public UnityEvent OnDoorOpen;
    public UnityEvent OnDoorClose;
    public UnityEvent OnOpenLockedDoor;

    public string Name => _name;
    public bool IsAnimating => _isAnimating;

    [ContextMenu("Interact Door")]
    public void Interact(PlayerCharacter character)
    {
        if (_isLocked == true)
        {
            bool haskey = character.Inventory.CheckItem(_keyID);
            if (haskey == true)
            {
                _isLocked = false;
                Open();
            }
            else
            {
                OnOpenLockedDoor?.Invoke();
            }
        }
        else
        { 
            if (_isOpen == true)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    public virtual void Open()
    {
        _isOpen = true;
        OnDoorOpen?.Invoke();
    }

    public virtual void Close()
    {
        _isOpen = false;
        OnDoorClose?.Invoke();
    }
}
