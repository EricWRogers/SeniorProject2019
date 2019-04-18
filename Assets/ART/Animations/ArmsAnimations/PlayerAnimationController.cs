using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public GameObject pause;

    void Strat()
    {

    }

    void PauseAnimation()
    {
        pause.GetComponent<PauseMenu>().Pause();
    }
}
