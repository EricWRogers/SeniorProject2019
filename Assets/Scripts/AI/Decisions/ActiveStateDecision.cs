﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool chaseTargetIsVisible = controller.gameManager.PlayerGO.transform.gameObject.activeSelf;
        return chaseTargetIsVisible;
    }
}