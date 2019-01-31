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

    private bool Look(StateController controller)
    {
        //looks for the player
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange,Color.green);
        
        if (Physics.Raycast(controller.eyes.position, controller.eyes.forward, out hit, controller.enemyStats.lookRange)
            && hit.collider.CompareTag("Player"))
        {
            //controller.gameManager.PlayerGO = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }

}
