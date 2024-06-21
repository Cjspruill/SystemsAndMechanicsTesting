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
        Invoke("DisableAnimation", timeInAnimation);
    }

    public void WalkBackwards()
    {
        animator.SetInteger("Direction", 1);
        Invoke("DisableAnimation", timeInAnimation);
    }

    public void StrafeLeft()
    {
        animator.SetInteger("Direction", 3);
        Invoke("DisableAnimation", timeInAnimation);
    }

    public void StrafeRight()
    {
        animator.SetInteger("Direction", 4);
        Invoke("DisableAnimation", timeInAnimation);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        Invoke("DisableAnimation", timeInAnimation);
    }

    void DisableAnimation()
    {
        animator.SetInteger("Direction", 0);
    }
}
