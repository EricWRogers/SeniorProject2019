using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInputManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if((InputManager.instance.Move().x > 0) || (InputManager.instance.Move().y > 0) || (InputManager.instance.Move().z > 0))
        {
            Debug.Log("Move: " + InputManager.instance.Move());
        }

        if((InputManager.instance.Looking().x > 0) || (InputManager.instance.Looking().y > 0) || (InputManager.instance.Looking().z > 0))
        {
            Debug.Log("Looking: " + InputManager.instance.Looking());
        }

        if(InputManager.instance.Interact())
        {
            Debug.Log("Interact");
        }

        if(InputManager.instance.Submit())
        {
            Debug.Log("Submit");
        }

        if(InputManager.instance.Cancel())
        {
            Debug.Log("Cancel");
        }

        if(InputManager.instance.Pause())
        {
            Debug.Log("Pause");
        }

        if(InputManager.instance.Sprint())
        {
            Debug.Log("Sprint");
        }
        
    }
}
