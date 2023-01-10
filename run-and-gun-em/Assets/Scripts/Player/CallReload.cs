using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallReload : StateMachineBehaviour
{
    public PlayerWeapon playerWeapon;

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerWeapon)
        {
            playerWeapon.OnReloadFinished();
        }
    }
}
