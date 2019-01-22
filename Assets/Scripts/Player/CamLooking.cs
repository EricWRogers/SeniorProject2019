using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLooking : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothv;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    GameObject character;
    void Start()
    {
        character = this.transform.parent.gameObject;
    }
    void Update()
    {
        var md = new Vector2(InputManager.instance.Looking().x, InputManager.instance.Looking().y);

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothv.x = Mathf.Lerp(smoothv.x, md.x, 1f / smoothing);
        smoothv.y = Mathf.Lerp(smoothv.y, md.y, 1f / smoothing);
        mouseLook += smoothv;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.left);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.zero);

        
    }
}
