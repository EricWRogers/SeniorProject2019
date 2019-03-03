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
    public float depletingSpeed = 10f;
    public float depletingCap = 50.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController CharController;
    private bool iscrouching = false;
    private bool isSprinting = false;
    private bool needCharging = false;
    private bool stopMoving = false;
    private Vector3 playerSize;
    GameManager gameManager;
    Rigidbody rigidbody;
    GameObject hud;
    GameObject pauseUI;
    GameObject Enemy;
    Scene scene;
    AudioSource insanity;



    void Awake()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        Enemy = GameObject.FindGameObjectWithTag("entity");
        hud = GameObject.Find("HUD");
        pauseUI = GameObject.Find("Pause UI");
        scene = SceneManager.GetActiveScene();
        playerSize = transform.localScale;
        gravityHolder = gravity;
    }

    void Start()
    {
        CharController = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        AudioManager.instance.CreateAudioSource("Walking", this.gameObject);
        originalSpeed = speed;
        insanity = GetComponent<AudioSource>();
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

        InsanitySound();

        GameOver();
    }

    private void Movement()
    {
        moveDirection = new Vector3(InputManager.instance.Move().x, 0.0f, InputManager.instance.Move().z);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        if (CharController.velocity.magnitude > 2f && CharController.isGrounded)
        {
            AudioManager.instance.PlayLocationSound("Walking");
        }
    }
    private void InsanitySound()
    {
        float dist = Vector3.Distance(this.transform.position, gameManager.EntityGO.transform.position);
        if (dist <= 400f)
            insanity.volume += .5f / dist;
        else
            insanity.volume -= .5f / dist;
        if (insanity.volume <= 0)
        {
            insanity.volume = 0;
        }
        if (insanity.volume >= 1)
        {
            insanity.volume = 1;
        }
    }
    private void Sprinting()
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

        if (gameManager.canDie)
        {
            if (other.tag == "entity")
            {
                Debug.Log("Death!!");
                stopMoving = true;
                GetComponentInChildren<CamLooking>().enabled = false;
                pauseUI.GetComponent<PauseMenu>().enabled = false;
                hud.GetComponent<HUD>().ReloadSceneLose();
            }
        }

        if(!gameManager.stopTimer && other.tag == "Finish")
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

    private void GameOver()
    {
        if (gameManager.stopTimer && gameManager.TimerSet <= 0)
        {
            Debug.Log("Game Over!!!");
            Enemy.SetActive(false);
            stopMoving = true;
            GetComponentInChildren<CamLooking>().enabled = false;
            pauseUI.GetComponent<PauseMenu>().enabled = false;
            hud.GetComponent<HUD>().ReloadSceneLose();
        }
    }
}
