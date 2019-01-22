using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMovement : MonoBehaviour
{
    public float speed = 0f;
    public float startYPos = -470.0f;
    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
        pos.y = startYPos;
        transform.position = pos;
    }

    void Update()
    {
        pos.y += speed / 100;
        transform.position = pos;
    }

    public void ResetPosition()
    {
        pos = transform.position;
        pos.y = startYPos;
        transform.position = pos;
    }
}
