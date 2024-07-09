using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleGrenade : MonoBehaviour
{
    [SerializeField] float cookTime; //How long before this blows up?
    [SerializeField] SphereCollider sphereColliderTrigger; //Grabs all the components to apply forces to them.
    [SerializeField] ParticleSystem explosionParticles; //Explosion particles reference
    [SerializeField] float explosionForce; //How much force does this produce?

    [SerializeField] float blackHoleTime;


    [SerializeField] bool lockPosition;
    [SerializeField] Vector3 positionToLockTo;
    [SerializeField] AudioSource audioSource;
    

    private void Update()
    {
        if (lockPosition)
        {
            transform.position = positionToLockTo;
        }
    }
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
        if (other.CompareTag("Grenade"))  return;

        if (other.GetComponent<Rigidbody>() != null)
        {
            if (other.GetComponent<BlackHoleGrenadeAttractor>() != null)
            {
                other.GetComponent<BlackHoleGrenadeAttractor>().blackHoleGrenade = this;
                lockPosition = true;
                positionToLockTo = transform.position;
            }
            Destroy(gameObject, blackHoleTime);
        }
    }
}
