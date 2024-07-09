using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float cookTime; //How long before this blows up?
    [SerializeField] SphereCollider sphereColliderTrigger; //Grabs all the components to apply forces to them.
    [SerializeField] ParticleSystem explosionParticles; //Explosion particles reference
    [SerializeField] float explosionForce; //How much force does this produce?
    [SerializeField] AudioSource audioSource;
    public void ThrowGrenade()
    {
        Invoke("Explode", cookTime);
    }

    void Explode()
    {
        audioSource.Play();
        explosionParticles.Play();
        sphereColliderTrigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grenade")) return;

        if (other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, sphereColliderTrigger.radius);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject,2);
        }
    }
}
