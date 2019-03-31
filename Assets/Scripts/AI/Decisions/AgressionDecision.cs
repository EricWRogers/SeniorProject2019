using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Agression")]
public class AgressionDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool waypointFull = Check(controller);
        return waypointFull;
    }

    private bool Check(StateController controller)
    {
        bool retValue = false;

        if(controller.gameManager.fullWaypoint != null)
        {
            Debug.Log("going aggression");
            retValue = true;
        }

        if(controller.throwableObject)
        {
            Debug.Log("going aggression");
            retValue = true;
            float dist = Vector3.Distance(controller.navMeshAgent.transform.position,controller.throwableObject.Throwable);
            if(dist < 15f)
            {
                retValue = false;
            }

        }

        return retValue;
    }

}
