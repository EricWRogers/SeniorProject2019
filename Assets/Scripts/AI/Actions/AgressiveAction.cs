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
        if(controller.throwableObject)
        {
            Debug.Log("going to object Thrown");
            controller.navMeshAgent.SetDestination(controller.throwableObject.Throwable);
        }

        if (controller.gameManager.fullWaypoint != null)
        {
            Debug.Log("going to Waypoint that playre");
            controller.navMeshAgent.SetDestination(controller.gameManager.fullWaypoint.transform.position);
        }
    }

    public override void OnStateExit(StateController controller)
    {
        base.OnStateExit(controller);
        controller.navMeshAgent.speed = controller.normalSpeed;
        controller.throwableObject = null;
    }
    
}
