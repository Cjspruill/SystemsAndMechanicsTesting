using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBangGrenade : MonoBehaviour
{
    [SerializeField] float cookTime; //How long before this blows up?
    [SerializeField] SphereCollider sphereColliderTrigger; //Grabs all the components to apply forces to them.
    [SerializeField] ParticleSystem explosionParticles; //Explosion particles reference
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
        if (other.CompareTag("Grenade") || other.CompareTag("Player") || other.CompareTag("Ignore")) return;

        if (other.GetComponent<AISwitchCase>() != null)
        {
            other.GetComponent<AISwitchCase>().SetConfusedState();
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 2);
        }
    }
}
