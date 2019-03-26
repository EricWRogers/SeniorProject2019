using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        GameObject player = GameObject.FindGameObjectWithTag ("Player");

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);
        float distToTarget = Vector3.Distance(controller.eyes.transform.position, controller.gameManager.PlayerGO.transform.position);

        if (distToTarget < 25 )
        {
            Debug.Log("Attacked");
            controller.gameManager.adrenalineAttacked();
            Vector3 moveDirection = controller.gameManager.PlayerGO.transform.position - controller.transform.position;
            controller.gameManager.PlayerGO.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * controller.enemyStats.attackPower);
            //play animation
            //check collision on state in animator
        }
    }
}