using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float originalSpeed;
    public float speed;
    public float gravity = 20.0f;
    private float gravityHolder;
    public float sprintMeater = 100.0f;
    public float depletingSpeed = 0.5f;
    public float depletingCap = 50.0f;
    public bool CanDie = true;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private bool iscrouching = false;
    private bool isSprinting = false;
    private Vector3 playerSize;
    GameManager GameManager;
    GameObject hud;
    GameObject Enemy;
    Scene scene;


    void Awake()
    {
        GameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        Enemy = GameObject.FindGameObjectWithTag("entity");
        hud = GameObject.Find("HUD");
        scene = SceneManager.GetActiveScene();
        playerSize = transform.localScale;
        gravityHolder = gravity;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalSpeed = speed;
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            Crouching();
            Sprinting();
            Movement();
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        GameOver();
    }

    private void Movement()
    {
            moveDirection = new Vector3(InputManager.instance.Move().x, 0.0f, InputManager.instance.Move().z);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
    }

    private void Sprinting()
    {
        if (InputManager.instance.Sprint() && sprintMeater >= depletingCap)
        {
            isSprinting = true;

            if(sprintMeater <= 100.0f && sprintMeater > 0.0f && !iscrouching)
            {
                //speed = originalSpeed * (2.0f * ( sprintMeater * 0.01f ));
                speed = originalSpeed * 2.0f;
                sprintMeater -= depletingSpeed;

                if (speed < originalSpeed)
                {
                    speed = originalSpeed;
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
                sprintMeater += depletingSpeed;
            }

            isSprinting = false;
        }
    }

    private void Crouching()
    {
        if(!isSprinting)
        {
            if(InputManager.instance.Crouch())
            {
                iscrouching = true;
                gravity = gravityHolder * 8f; 
                Vector3 sizeHolder = playerSize;
                sizeHolder.y /= 2;

                transform.localScale = sizeHolder;
            }
            else
            {
                iscrouching = false;
                gravity = gravityHolder;
                transform.localScale = playerSize;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (CanDie)
        {
            if (other.tag == "entity")
            {
                Debug.Log("Death!!");
                Enemy.SetActive(false);
                speed = 0;
                GetComponentInChildren<CamLooking>().enabled = false;
                hud.GetComponent<HUD>().ReloadSceneLose();
            }
        }

        if(GameManager.TimerSet > 0 && other.tag == "Finish")
        {
            Debug.Log("Win!!!");
            Enemy.SetActive(false);
            speed = 0;
            GetComponentInChildren<CamLooking>().enabled = false;
            hud.GetComponent<HUD>().ReloadSceneWin();
        }
    }

    private void GameOver()
    {
        if (GameManager.TimerSet <= 0)
        {
            Enemy.SetActive(false);
            speed = 0;
            GetComponentInChildren<CamLooking>().enabled = false;
            hud.GetComponent<HUD>().ReloadSceneLose();
        }
    }
}
