using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLooking : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivivty;

    float xAxisClamp = 0.0f;

    void Awake()
    {
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
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;
        
        if(xAxisClamp > 30)
        {
            xAxisClamp = 30;
            targetRotCam.x = 30;
        }
        else if (xAxisClamp < -30)
        {
            xAxisClamp = -30;
            targetRotCam.x = -30;
        }

        transform.rotation = Quaternion.Euler(targetRotCam);
        playerBody.rotation = Quaternion.Euler(targetRotBody);
    }
}
