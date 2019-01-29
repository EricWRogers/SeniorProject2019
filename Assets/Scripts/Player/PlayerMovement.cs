using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float originalSpeed = 10.0f;
    public float speed;
    public float gravity = 20.0f;
    public float sprintMeater = 100.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private bool iscrouching = false;
    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = originalSpeed;
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            if (InputManager.instance.Sprint())
            {
                isSprinting = true;

                if(sprintMeater <= 100.0f && sprintMeater > 0.0f)
                {
                    //speed = originalSpeed * (2.0f * ( sprintMeater * 0.01f ));
                    speed = originalSpeed * 2.0f;
                    sprintMeater -= 0.5f;

                    if (speed < 10.0f)
                    {
                        speed = 10.0f;
                    }
                }
                else if (sprintMeater <= 0.0f)
                {
                    sprintMeater = 0.0f;
                    speed = originalSpeed;
                }
            }
            else
            {
                speed = originalSpeed;

                if(sprintMeater >= 100.0f)
                {
                    sprintMeater = 100.0f;
                }
                else
                {
                    sprintMeater += 0.5f;
                }

                isSprinting = false;
            }

            if(InputManager.instance.Crouch())
            {
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }



            moveDirection = new Vector3(InputManager.instance.Move().x, 0.0f, InputManager.instance.Move().z);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
