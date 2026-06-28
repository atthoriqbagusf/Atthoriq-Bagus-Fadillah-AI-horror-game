using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField]
    private ItemData _data;

    public UnityEvent onItemPicked;
    public string Name => _data.Name;

    [ContextMenu("Interact Item")]
    public void Interact(PlayerCharacter character)
    {
        PickUp(character);
    }

    public virtual void PickUp(PlayerCharacter character)
    {
        ItemData newData = new ItemData(_data.ID, _data.Name);
        character.Inventory.AddItems(newData);
        onItemPicked?.Invoke();
        Destroy(gameObject);
    }
}
