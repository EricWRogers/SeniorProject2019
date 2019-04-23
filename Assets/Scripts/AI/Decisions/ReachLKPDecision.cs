using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/ReachLKP")]
public class ReachLKPDecision : Decision
{
    public float reachDistance = 10f;
    public override bool Decide(StateController controller)
    {
        bool ReachedLPK = LKP(controller);
        return ReachedLPK;
    }

    private bool LKP(StateController controller)
    {     
        float dist = Vector3.Distance(controller.gameManager.EntityGO.transform.position, controller.lastKnownPosition);
        if (dist <= reachDistance) {
            return true;
        }

        return false;
    }
}