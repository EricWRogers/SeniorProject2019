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
        controller.navMeshAgent.speed += .3f * Time.deltaTime;
        
        controller.navMeshAgent.destination = controller.gameManager.PlayerGO.transform.position;
        controller.navMeshAgent.isStopped = false;

    }
    public override void OnStateExit(StateController controller)
    {
        base.OnStateExit(controller);
        controller.navMeshAgent.speed = controller.normalSpeed;
    }
}