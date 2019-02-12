using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Wander")]
public class WanderDecision : Decision
{
    public float destDistanceStopping = 3f;
    public override bool Decide(StateController controller)
    {
        bool reachedWaypoint = Wander(controller);
        return reachedWaypoint;
    }

    private bool Wander(StateController controller)
    {
        bool retValue = false;
        float distFromPoint = 0;

        if (controller.navMeshAgent.destination != null)
        {
            distFromPoint = Vector3.Distance(controller.transform.position, controller.navMeshAgent.destination);
        }

        if (distFromPoint <= destDistanceStopping)
        {
            retValue = true;

        }
          
            return retValue;
    }
}

