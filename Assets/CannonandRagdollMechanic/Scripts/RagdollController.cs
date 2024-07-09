using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{

    [SerializeField] Rigidbody[] rigidbodies;   //Array of rigidbodies, set on start
    [SerializeField] Animator animator; //Animator reference
    // Start is called before the first frame update
    void Start()
    {
        //Set rigidbodies to get components in children, then set ragdoll inactive so it doesnt interfere with our capsule collider
      //  rigidbodies = GetComponentsInChildren<Rigidbody>();

        SetRagdollInactive();
    }

    //Sets ragdoll inactive
    void SetRagdollInactive()
    {
        //Loops through rigidbodies and sets is kinematic to true
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = true;
        }
    }

    //Set ragdoll active
    public void SetRagdollActive()
    {
        //Disable animator
        animator.enabled = false;

        //Loop through rigidbodies and turn off is kinematic and zero out its velocity so it doesnt snap around
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = false;
            rigidbodies[i].velocity = Vector3.zero;
        }
    }
}
