using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLooking : MonoBehaviour
{
    private GameObject Player;
    public float mouseSensitivivty;
    public float Xmax = 30.0f;
    public float Xmin = -30.0f;

    float xAxisClamp = 0.0f;

    void Awake()
    {
        Player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
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

        transform.rotation = Quaternion.Euler(targetRotCam);
        Player.transform.rotation = Quaternion.Euler(targetRotBody);
    }
}
