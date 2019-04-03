using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float pantingSpeed;
    public float sprintMeater = 100.0f;
    public float depletingSpeed = 10f;
    public float depletingCap = 50.0f;

    public bool stopMoving = false;

    public AudioClip walkingClip;
    public AudioClip sprintingClip;
    public AudioClip pantingClip;

    private float gravity = 20.0f;
    private float originalSpeed;
    private float gravityHolder;
    private float tempTime;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 playerSize;

    private bool iscrouching = false;
    private bool isSprinting = false;
    private bool doneSprinting = false;
    private bool needCharging = false;

    GameManager gameManager;
    AudioSource audioSource;
    RaycastHit hit;
    CharacterController CharController;

    void Awake()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        CharController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerSize = transform.localScale;
        gravityHolder = gravity;
        originalSpeed = speed;
        tempTime = Time.deltaTime;
    }

    void Update()
    {
        CheckIfHidden();
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
    }

    void Movement()
    {
        moveDirection = new Vector3(InputManager.instance.Move().x, 0.0f, InputManager.instance.Move().z);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        if (CharController.velocity.magnitude >= 2f)
        {
            if (Time.time >= tempTime + audioSource.clip.length && !isSprinting)
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
        if (InputManager.instance.Sprint() && !doneSprinting)
        {
            isSprinting = true;

            if (sprintMeater <= 100.0f && sprintMeater > 0.0f && !iscrouching && !needCharging)
            {
                audioSource.clip = sprintingClip;

                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

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
                doneSprinting = true;
            }
        }
        else
        {

            if (sprintMeater >= 100.0f)
            {
                sprintMeater = 100.0f;
            }
            else if(!doneSprinting)
            {
                sprintMeater += depletingSpeed * Time.deltaTime;
            }

            if (sprintMeater <= depletingCap)
            {
                needCharging = true;
                doneSprinting = false;
                speed = pantingSpeed;

                audioSource.clip = pantingClip;

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                needCharging = false;
                speed = originalSpeed;

                audioSource.clip = walkingClip;

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
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

    void CheckIfHidden()
    {
        if (Physics.Raycast(transform.position, transform.up, out hit, 15f))
        {
            if(hit.collider.tag == "HideableObjects")
            {
                Debug.Log(hit.collider.name);
                gameObject.layer = LayerMask.NameToLayer("obstacles");
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("target");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "KeyCard")
        {
            if(!(GetComponent<KeyChain>().KeysInPocket.Contains(other.gameObject.GetComponent<KeyCard>().KeyName)))
            {
                GetComponent<KeyChain>().KeysInPocket.Add(other.gameObject.GetComponent<KeyCard>().KeyName); 
                Destroy(other.gameObject); 
            }
            else
            {
                Destroy(other.gameObject);
            }
        }

        if(other.tag == "Finish")
        {
            gameManager.Win();
        }
    }
}
