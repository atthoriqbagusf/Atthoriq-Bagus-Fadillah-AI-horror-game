using UnityEngine;

public class DropObjectGameEvent : GameEventBase
{
    [SerializeField]
    private Rigidbody _dropObjectPhysics;

    public override void Trigger()
    {
        _dropObjectPhysics.useGravity = true;
        base.Trigger();
    }
}
