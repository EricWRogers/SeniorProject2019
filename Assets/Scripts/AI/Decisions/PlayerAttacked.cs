using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/PlayerAttacked")]
public class PlayerAttacked : Decision
{
   public override bool Decide(StateController controller)
    {
        bool playerAttacked = (controller);
        return playerAttacked;
    }
    private bool CheckAttack(StateController controller)
    {
        bool retValue = false;
        if(controller.gameManager.playerAttacked)
        {
            retValue = true;

        }
        return retValue;
    }
}
