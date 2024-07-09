using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorWithButtonInputs : MonoBehaviour
{
    [SerializeField] Animator animator; //Animator component to animate
    [SerializeField] float timeInAnimation; //How long should we loop this animation?

    public void WalkForward()
    {
        animator.SetInteger("Direction", 2);
        animator.ResetTrigger("Attack");
        StartCoroutine("DisableAnimation");
    }

    public void WalkBackwards()
    {
        animator.SetInteger("Direction", 1);
        animator.ResetTrigger("Attack");
        StartCoroutine("DisableAnimation");
    }

    public void StrafeLeft()
    {
        animator.SetInteger("Direction", 3);
        animator.ResetTrigger("Attack");
        StartCoroutine("DisableAnimation");
    }

    public void StrafeRight()
    {
        animator.SetInteger("Direction", 4);
        animator.ResetTrigger("Attack");
        StartCoroutine("DisableAnimation");
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        StartCoroutine("DisableAnimation");
    }

   IEnumerator DisableAnimation()
    {
        yield return new WaitForSeconds(timeInAnimation);
        animator.ResetTrigger("Attack");
        animator.SetInteger("Direction", 0);
    }
}
