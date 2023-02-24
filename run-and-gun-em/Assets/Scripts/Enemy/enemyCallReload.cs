using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCallReload : StateMachineBehaviour
{
    public EnemyWeapon enemyWeapon;

    //Körs då animation "Reload" är klar
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyWeapon)
        {
            animator.ResetTrigger("Reload");
            enemyWeapon.OnReloadFinished();
        }
    }
}
