using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Transform pause;
    public Transform player;
    public Transform playerCam;

    void Strat()
    {

    }

    void PauseAnimation()
    {
        pause.GetComponent<PauseMenu>().Pause();
        player.GetComponent<PlayerMovement>().stopMoving = true;
        playerCam.GetComponent<CamLooking>().enabled = false;
        playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, pause.transform.position, 0.2f);
    }

    void ResumeAnimation()
    {
        pause.GetComponent<PauseMenu>().Resume();
        player.GetComponent<PlayerMovement>().stopMoving = false;
        playerCam.GetComponent<CamLooking>().enabled = true;
    }
}
