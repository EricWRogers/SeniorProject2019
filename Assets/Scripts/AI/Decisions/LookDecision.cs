using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }
    //layermask for hiding player and what not
 
    private bool Look(StateController controller)
    {
        bool retValue = false;
        Collider[] inViewRadius = Physics.OverlapSphere(controller.eyes.transform.position, controller.viewRadius,controller.playerTargetMask);

        for (int i = 0; i< inViewRadius.Length; i++)
        {
            Transform target = inViewRadius[i].transform;
            Vector3 dirToTarget = (controller.gameManager.PlayerGO.transform.position - controller.eyes.transform.position).normalized;
            if(Vector3.Angle(controller.eyes.transform.forward, dirToTarget)< controller.viewAngle /2 )
            {
                float distToTarget = Vector3.Distance(controller.eyes.transform.position, target.position);
                if(!Physics.Raycast(controller.eyes.transform.position,dirToTarget,distToTarget, controller.ObstacleMask))
                {
                    retValue = true;
                }
            }
            if (Vector3.Angle(controller.eyes.transform.forward * -1, dirToTarget) < controller.viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(controller.eyes.transform.position, target.position);
                if (!Physics.Raycast(controller.eyes.transform.position, dirToTarget, distToTarget, controller.ObstacleMask))
                {
                    retValue = true;
                }
            }

        }
 
        return retValue;


    }
     

}
       
     


   
