using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallReload : StateMachineBehaviour
{
    public PlayerWeapon playerWeapon;

    //Körs då animation "Reload" är klar
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerWeapon)
        {
            playerWeapon.OnReloadFinished();
        }
    }
}
