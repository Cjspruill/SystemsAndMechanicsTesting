using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    [SerializeField] Rigidbody[] rigidbodies;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        SetRagdollInactive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRagdollInactive()
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = true;
        }
    }

    public void SetRagdollActive()
    {
        animator.enabled = false;

        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].velocity = Vector3.zero;
        }
    }
}
