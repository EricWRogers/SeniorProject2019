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
            Debug.Log("EntityGOingToFurthestPoint"+ controller.gameManager.tMax.transform);
            float dist =Vector3.Distance(controller.transform.position,controller.gameManager.PlayerGO.transform.position);
            Debug.Log("going to "+controller.gameManager.tMax);
            bool result = controller.navMeshAgent.SetDestination(controller.gameManager.tMax.transform.position);
            if(dist > 150)
            {
                Debug.Log("player Wake Up");
                controller.gameManager.WakeUp();
            }
        }
        if (!controller.navMeshAgent.hasPath && !controller.navMeshAgent.pathPending)
        {  
            bool result = controller.navMeshAgent.SetDestination(controller.gameManager.RoomGOS[controller.nextWayPoint = Random.Range(0, controller.gameManager.RoomGOS.Length)].transform.position);
        }

    }
}