using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float sprintMeater = 100.0f;
    public float depletingSpeed = 10f;
    public float depletingCap = 50.0f;

    public bool attacked = false;

    private float gravity = 20.0f;
    private float originalSpeed;
    private float gravityHolder;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 playerSize;

    private bool iscrouching = false;
    private bool isSprinting = false;
    private bool needCharging = false;
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

        if (CharController.velocity.magnitude < 2f && CharController.isGrounded)
        {
            audioSource.Play();
        }
    }

    void Sprinting()
    {
        if (InputManager.instance.Sprint())
        {
            isSprinting = true;

            if(sprintMeater <= 100.0f && sprintMeater > 0.0f && !iscrouching && !needCharging)
            {
                //speed = originalSpeed * (2.0f * ( sprintMeater * 0.01f ));
                speed = originalSpeed * 2.0f;
                sprintMeater -= depletingSpeed * Time.deltaTime;

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
                sprintMeater += depletingSpeed * Time.deltaTime;
            }

            if(sprintMeater <= depletingCap)
            {
                needCharging = true;
            }else
            {
                needCharging = false;
            }

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
            this.gameObject.GetComponent<KeyChain>().KeysInPocket.Add(other.gameObject.GetComponent<KeyCard>().KeyName);
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
