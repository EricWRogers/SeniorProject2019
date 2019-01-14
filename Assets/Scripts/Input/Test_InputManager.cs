using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_InputManager : MonoBehaviour {
    private void Update()
    {
        Debug.Log(InputManager.Joystick_Move());
        Debug.Log(InputManager.Joystick_Looking());

        if (InputManager.Pause())
        {
            Debug.Log(InputManager.Pause());
        }

        if (InputManager.Submit())
        {
            Debug.Log(InputManager.Submit());
        }

        if(InputManager.Cancel())
        {
            Debug.Log(InputManager.Cancel());
        }
    }
}