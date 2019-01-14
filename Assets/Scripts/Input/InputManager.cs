using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputManager : MonoBehaviour {

    public static InputManager instance;

    private void Awake()
    {
        instance = this;
    }

    //Movement
    private static float Move_Horizontal()
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

    private static float Move_Vertical()
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

    public static Vector2 Joystick_Move()
    {
        return new Vector2(Move_Horizontal(), Move_Vertical());
    }

    //Looking
    private static float Looking_Horizontal()
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

    private static float Looking_Vertical()
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

    public static Vector2 Joystick_Looking()
    {
        return new Vector2(Looking_Horizontal(), Looking_Vertical());
    }






    public static bool Jump()
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

            case OperatingSystemFamily.Windows:

                if(Input.GetAxis("Jump_Joystick_R_Trigger_Windows") > offset)
                {
                    return true;

                } else if (Input.GetButtonDown("Jump_Keyboard"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Linux:

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

    public static bool Pause()
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

    public static bool Submit()
    {
        if (Input.GetButtonDown("Submit"))
        {
            return true;
        }
        else return false;
    }

    public static bool Cancel()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            return true;
        }
        else return false;
    }
}