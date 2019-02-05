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

        Vector3 raydirection = controller.gameManager.PlayerGO.transform.position - controller.eyes.position;
        float DTP = Vector3.Distance(controller.gameManager.PlayerGO.transform.position, controller.eyes.transform.position);


        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange,Color.green);
        //Back
        if (Physics.Raycast(controller.eyes.position, raydirection, out hit))
        {
            if ((hit.transform.tag == "Player") && (DTP <= controller.enemyStats.minPlayerDetectDist))
            {
                Debug.Log("see Player");
                return true;
            }
        }
        //Front
        if (Vector3.Angle(raydirection, controller.eyes.transform.forward) <= controller.enemyStats.fovAngle && 
            Physics.Raycast(controller.eyes.position, controller.eyes.forward, out hit, controller.enemyStats.lookRange) && hit.collider.CompareTag("Player"))
        {
            Debug.Log("see Player");

            return true;
        }

        else
        {
            return false;
        }
    }

}
       
     


   
