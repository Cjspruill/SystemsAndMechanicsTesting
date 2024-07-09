using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityMine : MonoBehaviour
{
    [SerializeField] SphereCollider sphereColliderTrigger; //Grabs all the components to apply forces to them.
    [SerializeField] ParticleSystem explosionParticles; //Explosion particles reference
    [SerializeField] float explosionForce; //How much force does this produce?

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] AudioClip placementClip;

    private void OnCollisionEnter(Collision collision)
    {
        //transform.parent = collision.collider.transform;
        audioSource.clip = placementClip;
        audioSource.Play();
        transform.position = transform.position;
        transform.rotation = transform.rotation;
        sphereColliderTrigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grenade") || other.CompareTag("Ignore") || other.CompareTag("Player")) return;

        if (other.GetComponent<Rigidbody>() != null)
        {
            audioSource.clip = explosionClip;
            audioSource.Play();
            explosionParticles.Play();
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, sphereColliderTrigger.radius);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 2);
        }
    }
}
