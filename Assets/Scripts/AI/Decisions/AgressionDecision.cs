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
            retValue = true;
        }

        return retValue;
    }

}
