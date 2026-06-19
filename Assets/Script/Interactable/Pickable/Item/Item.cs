using UnityEngine;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField]
    private string _name;

    public string Name => _name;

    public void Interact()
    {
        // Implementation for item interaction
    }

    public void PickUp()
    {
        // Implementation for picking up the item
    }
}
