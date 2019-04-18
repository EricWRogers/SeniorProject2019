using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    GameObject pause;

    void Strat()
    {
        pause = transform.Find("Pause").gameObject;
    }

    void PauseAnimation()
    {
        pause.GetComponent<PauseMenu>().Pause();
    }
}
