using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLooking : MonoBehaviour
{
    public float mouseSensitivivty;
    public float Xmax = 30.0f;
    public float Xmin = -30.0f;
    public float speed = 100f;
    public float maxAngle = 20f;

    float xAxisClamp = 0.0f;

    GameObject Player;

    Vector3 camCenter;

    RaycastHit hitInfoLeft;
    RaycastHit hitInfoRight;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        camCenter = transform.localPosition;
    }

    void FixedUpdate()
    {
        Vector3 left = -transform.right;
        Vector3 right = transform.right;

        float MouseX = InputManager.instance.Looking().x;
        float MouseY = InputManager.instance.Looking().y;

        float rotAmountX = MouseX * mouseSensitivivty;
        float rotAmountY = MouseY * mouseSensitivivty;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = Player.transform.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;
        
        if(xAxisClamp > Xmax)
        {
            xAxisClamp = Xmax;
            targetRotCam.x = Xmax;
        }
        else if (xAxisClamp < Xmin)
        {
            xAxisClamp = Xmin;
            targetRotCam.x = Xmin;
        }

        if (InputManager.instance.Lean_L())
        {
            if (Physics.Raycast(transform.position, left, out hitInfoLeft, 6, Player.layer))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-hitInfoLeft.distance, 1, 0), 0.1f);
                Player.GetComponent<PlayerMovement>().stopMoving = true;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-1, 1, 0), 0.1f);
                Player.GetComponent<PlayerMovement>().stopMoving = true;
            }
        }
        else if(InputManager.instance.Lean_R())
        {
            if (Physics.Raycast(transform.position, right, out hitInfoRight, 6, Player.layer))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(hitInfoRight.distance, 1, 0), 0.1f);
                Player.GetComponent<PlayerMovement>().stopMoving = true;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(1, 1, 0), 0.1f);
                Player.GetComponent<PlayerMovement>().stopMoving = true;
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, camCenter, 0.1f);
            Player.GetComponent<PlayerMovement>().stopMoving = false;
        }

       transform.rotation = Quaternion.Euler(targetRotCam);
       Player.transform.rotation = Quaternion.Euler(targetRotBody);
    }
}
