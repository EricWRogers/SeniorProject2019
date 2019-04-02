using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public float maxDistance;
    public string OpenMessage;
    public string CloseMessage;

    bool showMessage;
    string message;
    GameObject playerGO;
    Animator animator;
    AudioSource audioSource;
    HUD hud;

    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        hud = GameObject.FindObjectOfType<HUD>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ShowLockerMessage();
    }

    void ShowLockerMessage()
    {
        float dist = Vector3.Distance(playerGO.transform.position, transform.position);

        if (dist <= maxDistance)
        {
            if (!animator.GetBool("IsLockerOpen"))
            {
                hud.MessageForPlayer(OpenMessage);

            }
            else
            {
                hud.MessageForPlayer(CloseMessage);
            }

            PlayAnimation();
        }
        else if (dist >= maxDistance)
        {
            hud.MessageForPlayer();
        }
    }

    void PlayAnimation()
    {
        if (InputManager.instance.Interact())
        {
            animator.SetTrigger("Open/Close");
        }
    }
}
