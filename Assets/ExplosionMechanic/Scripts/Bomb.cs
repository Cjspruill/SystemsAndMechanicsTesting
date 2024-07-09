using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] List<Rigidbody> rigidBodies;   //List of rigidbodies to affect
    public List<Rigidbody> RigidBodies { get => rigidBodies; set => rigidBodies = value; }  //Rigidbody property
    [SerializeField] float bombActivationTime;  //How long does it take the bomb to activate?
    [SerializeField] float explosionForce;  //Explosion force of the bomb
    [SerializeField] BombTrigger bombTrigger;   //Bomb trigger reference
    [SerializeField] ParticleSystem explosionParticles; //Particle system for the explosion
 
    // Update is called once per frame
    void Update()
    {
        //If we press e, set bombtrigger active and invoke the activate bomb function
        if (Input.GetKeyDown(KeyCode.E))
        {
            bombTrigger.gameObject.SetActive(true);
            Invoke("ActivateBomb", bombActivationTime);
        }
    }

    //Activate bomb
    void ActivateBomb()
    {
        //Play explosion
        explosionParticles.Play();

        //Loop through rigidbodies
        for (int i = 0; i < RigidBodies.Count; i++)
        {
            //Turn off is kinematic
            RigidBodies[i].isKinematic = false;
            //If we find an animator, disable it and set ragdoll to active
            if (RigidBodies[i].GetComponentInParent<Animator>() != null)
            {
                RigidBodies[i].GetComponentInParent<Animator>().enabled = false;
                RigidBodies[i].GetComponentInParent<RagdollController>().SetRagdollActive();
            }
            //Add explosion force to the rigidbody at the index
            RigidBodies[i].AddExplosionForce(explosionForce, transform.position, bombTrigger.GetComponent<SphereCollider>().radius);
        }
    }
}
