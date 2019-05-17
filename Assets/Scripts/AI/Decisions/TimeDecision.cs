using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Time")]
public class TimeDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool timeHasPassed = CheckTime(controller);
        return timeHasPassed;
    }

    private bool CheckTime(StateController controller)
    {
       bool retValue = false;

       if(controller.CheckIfCountDownElapsed(controller.enemyStats.wanderDuration))
        {
            retValue = true;
        }
        return retValue;
    }
}
