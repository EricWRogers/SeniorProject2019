using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float sprintingWait = 5.0f;
    public float acceleration = 3f;
    public float deceleration = 6f;

    public bool attacked = false;
    public bool maxSpeedReached = false;

    private float gravity = 20.0f;
    private float maxSpeed;
    private float originalSpeed;
    private float gravityHolder;
    private float sprintingWaitHolder;
    private float tempTime;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 playerSize;

    private bool iscrouching = false;
    private bool isSprinting = false;
    private bool stopMoving = false;

    GameObject Enemy;
    GameObject hud;
    GameObject pauseUI;

    GameManager gameManager;
    Scene scene;
    AudioSource audioSource;
    CharacterController CharController;

    void Awake()
    {
        Enemy = GameObject.FindGameObjectWithTag("entity");
        hud = GameObject.Find("HUD");
        pauseUI = GameObject.Find("Pause UI");
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        scene = SceneManager.GetActiveScene();
        CharController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerSize = transform.localScale;
        gravityHolder = gravity;
        originalSpeed = speed;
        sprintingWaitHolder = sprintingWait;
        tempTime = Time.deltaTime;
        maxSpeed = speed * 3;
    }

    void FixedUpdate()
    {
        if(!stopMoving)
        {
            if (CharController.isGrounded)
            {
                Crouching();
                Sprinting();
                Movement();
            }

            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
            CharController.Move(moveDirection * Time.deltaTime);
        }

        GameOver();
    }

    void Movement()
    {
        moveDirection = new Vector3(InputManager.instance.Move().x, 0.0f, InputManager.instance.Move().z);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        if (CharController.velocity.magnitude >= 2f)
        {
            if (Time.time >= tempTime + audioSource.clip.length || Time.time <= audioSource.clip.length)
            {
                audioSource.Play();
                tempTime = Time.time;
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void Sprinting()
    {
        if (InputManager.instance.Sprint())
        {
            isSprinting = true;

            if(!iscrouching)
            {
                if (speed <= maxSpeed && !maxSpeedReached)
                {
                    speed = speed + acceleration * Time.deltaTime;
                }
                else
                {
                    maxSpeedReached = true;
                }

                if (speed >= originalSpeed && maxSpeedReached)
                {
                    speed = speed - deceleration * Time.deltaTime;
                }

                if (speed <= originalSpeed)
                {
                    speed = originalSpeed;
                    maxSpeedReached = false;
                }
            }
        }
        else
        {
            speed = originalSpeed;
            maxSpeedReached = false;
            isSprinting = false;
        }
    }


    void Crouching()
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

                Vector3 sizeHolder = playerSize;

                if(transform.localScale != playerSize)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, sizeHolder, 0.1f);
                }
            }
        }
    }

    void GameOver()
    {
        if (attacked)
        {
            Debug.Log("Game Over!!!");
            Enemy.SetActive(false);
            stopMoving = true;
            GetComponentInChildren<CamLooking>().enabled = false;
            pauseUI.GetComponent<PauseMenu>().enabled = false;
            hud.GetComponent<HUD>().ReloadSceneLose();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "KeyCard")
        {
            GetComponent<KeyChain>().KeysInPocket.Add(other.gameObject.GetComponent<KeyCard>().KeyName);
            Destroy(other);
        }
        if(other.tag == "Finish")
        {
            Debug.Log("Win!!!");
            Enemy.SetActive(false);
            gameManager.stopTimer = true;
            stopMoving = true;
            GetComponentInChildren<CamLooking>().enabled = false;
            pauseUI.GetComponent<PauseMenu>().enabled = false;
            hud.GetComponent<HUD>().ReloadSceneWin();
        }
    }
}
