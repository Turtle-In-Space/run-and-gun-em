using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
    /*
     * Tar bort explosionen när animationen är klar
     */
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }
}
