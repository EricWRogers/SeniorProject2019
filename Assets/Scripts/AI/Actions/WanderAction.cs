﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Wander")]
public class WanderAction : Action
{
    public override void Act(StateController controller)
    {
        Wander(controller);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    private void Wander(StateController controller)
    {

        if (controller.CheckIfCountDownElapsed(controller.enemyStats.wanderDuration))
        {
            Vector3 newPos = RandomNavSphere(controller.transform.position, controller.enemyStats.wanderRadius, -1);
            controller.navMeshAgent.SetDestination(newPos);

        }

    }
}