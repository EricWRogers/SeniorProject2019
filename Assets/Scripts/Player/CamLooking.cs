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

    GameObject Player;
    float xAxisClamp = 0.0f;
    float curAngle = 0f;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
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
            curAngle = Mathf.MoveTowardsAngle(curAngle, maxAngle, speed * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-1, 0.8f, 0), 0.1f);
            //transform.localPosition = new Vector3(-1,0.8f,0);;
        }
        else if (InputManager.instance.Lean_R())
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, -maxAngle, speed * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(1, 0.8f, 0), 0.1f);
            //transform.localPosition = new Vector3(1,0.8f,0);
        }
        else
        {
            curAngle = Mathf.MoveTowardsAngle(curAngle, 0f, speed * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0.8f, 0), 0.1f);
            //transform.localPosition = new Vector3(0,0.8f,0);
        }

        if(InputManager.instance.Lean_L() || InputManager.instance.Lean_R())
        {
            transform.localRotation = Quaternion.AngleAxis(curAngle, Vector3.forward); 
        }
        else
        {
            transform.rotation = Quaternion.Euler(targetRotCam);
            Player.transform.rotation = Quaternion.Euler(targetRotBody);
        }
    }
}
