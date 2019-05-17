using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.navMeshAgent.speed += 1.5f * Time.deltaTime;
        
        controller.navMeshAgent.destination = controller.lastKnownPosition;
        controller.navMeshAgent.isStopped = false;

    }
    public override void OnStateExit(StateController controller)
    {
        base.OnStateExit(controller);
        controller.navMeshAgent.speed = controller.normalSpeed;
    }
}