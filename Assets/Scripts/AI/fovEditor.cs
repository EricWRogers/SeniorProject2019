using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class fovEditor : MonoBehaviour
{
    public LayerMask playerTargetMask;
    public LayerMask ObstacleMask;


    void OnSceneGUI(StateController controller)
    {
        Handles.color = Color.blue;
        Handles.DrawWireArc(controller.eyes.transform.position, Vector3.up,Vector3.forward,360,controller.enemyStats.lookRadius);
        Vector3 viewAngleA = controller.DirFromAngle(-controller.enemyStats.fovAngle / 2, false);
        Vector3 viewAngleB = controller.DirFromAngle(controller.enemyStats.fovAngle / 2, false);

        Handles.DrawLine(controller.eyes.transform.position, controller.eyes.transform.position + viewAngleA * controller.enemyStats.lookRadius);
        Handles.DrawLine(controller.eyes.transform.position, controller.eyes.transform.position + viewAngleB * controller.enemyStats.lookRadius);

    }
}
