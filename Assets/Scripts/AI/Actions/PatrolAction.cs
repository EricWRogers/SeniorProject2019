using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    float distFromPoint = 0;
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }


    private void Patrol(StateController controller)
    {
        
        controller.navMeshAgent.destination = controller.gameManager.RoomGOS[controller.nextWayPoint = Random.Range(0, controller.gameManager.RoomGOS.Length)].transform.position;
        controller.navMeshAgent.isStopped = false;

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.nextWayPoint = Random.Range(0, controller.gameManager.RoomGOS.Length);
        }

    }

}