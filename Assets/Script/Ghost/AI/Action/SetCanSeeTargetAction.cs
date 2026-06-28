using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Can See Target", story: "Set CanSee Target from [AI]", category: "Action", id: "f7e9a814a0e0630de8cdf54c3119b87e")]
public partial class SetCanSeeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanSeeTarget;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null || AI.Value.SightPerception == null)
        {
            return Status.Failure;
        }
        CanSeeTarget.Value = AI.Value.SightPerception.CanSeePlayer;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

