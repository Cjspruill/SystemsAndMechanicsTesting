using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class Titan : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] Animator animator;
    [SerializeField] Transform aiTargetDestination;
    [SerializeField] NavMeshAgent navMeshAgent;

    public float Health { get => health; set => health = value; }

    private void Start()
    {
        navMeshAgent.SetDestination(aiTargetDestination.position);
        animator.SetBool("IsWalking", true);
    }
   public void TakeDamage(float value) 
    {
        Health -= value;
        if (Health <= 0)
        {
            Health = 0;

        }
    }
}
