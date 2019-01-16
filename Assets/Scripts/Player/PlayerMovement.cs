using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10.0f;
    void Update()
    {
        float x = InputManager.instance.Move().x;
        float y = InputManager.instance.Move().z;

        x *= Time.deltaTime;
        y *= Time.deltaTime;

        transform.Translate(x, 0, y);
    }
}
