using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Agression")]
public class AgressiveAction : Action
{
    public override void Act(StateController controller)
    {
        Agressive(controller);
    }

    private void Agressive(StateController controller)
    {
        
        if (controller.gameManager.fullWaypoint != null)
        {
            controller.navMeshAgent.SetDestination(controller.gameManager.fullWaypoint.transform.position);
        }
    }

    public override void OnStateExit(StateController controller)
    {
        base.OnStateExit(controller);
        controller.navMeshAgent.speed = controller.normalSpeed;
    }
    
}
