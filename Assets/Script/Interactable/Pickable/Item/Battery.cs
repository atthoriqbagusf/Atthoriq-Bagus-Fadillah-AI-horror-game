using UnityEngine;

public class Battery : Item
{
    public override void PickUp(PlayerCharacter character)
    {
        base.PickUp(character);
        character.Flashlight.RefillBatteryLevel();
    }
}
