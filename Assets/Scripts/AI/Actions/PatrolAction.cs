using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
  
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }


    private void Patrol(StateController controller)
    {
        if(controller.gameManager.playerAttacked)
        {
            Debug.Log("going to "+controller.gameManager.tMax);
            bool result = controller.navMeshAgent.SetDestination(controller.gameManager.tMax.transform.position);
        }
        if (!controller.navMeshAgent.hasPath && !controller.navMeshAgent.pathPending)
        {  
            bool result = controller.navMeshAgent.SetDestination(controller.gameManager.RoomGOS[controller.nextWayPoint = Random.Range(0, controller.gameManager.RoomGOS.Length)].transform.position);
        }

    }
}