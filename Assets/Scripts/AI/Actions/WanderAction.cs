using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Wander")]
public class WanderAction : Action
{
    public float destDistanceThreshold = 3f;

    private class Data
    {
        public float lastWanderElapsedTime = 0;
    }

    Data data = new Data();

    public override void Act(StateController controller)
    {
        Wander(controller);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, out bool sampled)
    {
        Vector3 randPoint = origin + Random.insideUnitSphere * dist;
        

        NavMeshHit navHit;

        sampled = NavMesh.SamplePosition(randPoint, out navHit, 1.0f, NavMesh.AllAreas);
        

        return navHit.position;
    }

    private void Wander(StateController controller)
    {
        float distFromPoint = 0;
        if (controller.navMeshAgent.destination != null)
        {
            distFromPoint = Vector3.Distance(controller.transform.position, controller.navMeshAgent.destination);
        }

        if (distFromPoint <= destDistanceThreshold)
        {
        
            if (data.lastWanderElapsedTime > controller.enemyStats.searchDuration)
            {
                bool sampled;
                Vector3 newPos = RandomNavSphere(controller.transform.position, controller.enemyStats.wanderRadius, out sampled);
                if (sampled)
                {
                  
                    controller.navMeshAgent.SetDestination(newPos);
                }
            } else
            {
                data.lastWanderElapsedTime += Time.deltaTime;
            }
        }
        else
        {
            data.lastWanderElapsedTime = 0;
        }
    }

    public new void OnStateEnter()
    {
        base.OnStateEnter();
        data.lastWanderElapsedTime = 0;
    }

    public new void OnStateExit()
    {
        base.OnStateExit();
    }
}
