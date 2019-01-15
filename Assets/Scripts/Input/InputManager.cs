using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    private void Awake()
    {
        instance = this;
    }

    //Movement
    private float Move_Horizontal()
    {
        float r = 0.0f;

        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Horizontal");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Horizontal");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("Horizontal");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }

    }

    private float Move_Vertical()
    {
        float r = 0.0f;

        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Vertical");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Vertical");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("Vertical");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }
    }

    public Vector3 Move()
    {
        Vector3 test = new Vector3(Move_Horizontal(),0, Move_Vertical());
        
        if (test == Vector3.zero)
        {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");

            return new Vector3 (moveHorizontal, 0.0f, moveVertical);

        }else
        {
            return new Vector3(Move_Horizontal(),0, Move_Vertical());
        }
    }

    //Looking
    private float Looking_Horizontal()
    {
        float r = 0.0f;
        
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Looking_Horizontal_Joystick_MacOSX");
        return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Looking_Horizontal_Joystick_Windows");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("looking_Horizontal_Joystick_Linux");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }
    }

    private float Looking_Vertical()
    {
        float r = 0.0f;
        
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Looking_Vertical_Joystick_MacOSX");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Looking_Vertical_Joystick_Windows");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("looking_Vertical_Joystick_Linux");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }
    }

    public Vector3 looking()
    {
        Vector3 test = new Vector3(Looking_Horizontal(), Looking_Vertical(), 0);

        if(test == Vector3.zero)
        {
            return Vector3.zero;
        }else
        {
            return Vector3.zero;
        }
    }

    public bool Jump()
    {
        float offset = 0.75f;

        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                if(Input.GetAxis("Jump_Joystick_R_Trigger_MacOSX") > offset)
                {
                    return true;

                } else if (Input.GetButtonDown("Jump_Keyboard"))
                {
                     return true;
                }
                else return false;

            case 
               OperatingSystemFamily.Windows:

                if(Input.GetAxis("Jump_Joystick_R_Trigger_Windows") > offset)
                {
                    return true;

                } else if (Input.GetButtonDown("Jump_Keyboard"))
                {
                     return true;
                }
                else return false;

            case 
               OperatingSystemFamily.Linux:

                if(Input.GetAxis("Jump_Joystick_R_Trigger_Linux") > offset)
                {
                    return true;

                } else if (Input.GetButtonDown("Jump_Keyboard"))
                {
                     return true;
                }
                else return false;

            default:
                return false;
        }
    }

    public bool Pause()
    {
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                if (Input.GetButtonDown("Pasue_Joystick_MacOSX"))
                {
                    return true;
                }
                else if (Input.GetButtonDown("Pasue_Keyboard"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Windows:

                if (Input.GetButtonDown("Pasue_Joystick_Windows"))
                {
                    return true;
                }
                else if (Input.GetButtonDown("Pasue_Keyboard"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Linux:
        
                if (Input.GetButtonDown("Pasue_Joystick_Linux"))
                {
                    return true;
                }
                if (Input.GetButtonDown("Pasue_Keyboard"))
        {
                    return true;
                }
                else return false;
        
            default:
                return false;
        }
    }

    public bool Submit()
    {
        if (Input.GetButtonDown("Submit"))
        {
            return true;
        }
        else return false;
    }

    public bool Cancel()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            return true;
        }
        else return false;
    }
}