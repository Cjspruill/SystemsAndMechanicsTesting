
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyGrenade : MonoBehaviour
{
    [SerializeField] float cookTime; //How long before this blows up?
    [SerializeField] SphereCollider sphereColliderTrigger; //Grabs all the components to apply forces to them.
    [SerializeField] ParticleSystem explosionParticles; //Explosion particles reference
    [SerializeField] float explosionForce; //How much force does this produce?

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] AudioClip placementClip;
   public void ThrowGrenade()
    {
        Invoke("Explode", cookTime);
    }

    void Explode()
    {
        audioSource.clip = explosionClip;
        audioSource.Play();
        explosionParticles.Play();
        sphereColliderTrigger.enabled = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Brick")|| collision.collider.CompareTag("Metal") || collision.collider.CompareTag("Concrete") || collision.collider.CompareTag("Ground"))
        {
            audioSource.clip = placementClip;
            audioSource.Play();
         //   transform.parent = collision.collider.transform;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = transform.position;
            transform.rotation = transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grenade")) return;

        if (other.GetComponent<Rigidbody>()!=null)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, sphereColliderTrigger.radius);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject,2);
        }
    }

}
