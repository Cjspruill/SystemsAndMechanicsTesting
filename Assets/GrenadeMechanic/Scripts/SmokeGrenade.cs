using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenade : MonoBehaviour
{

    [SerializeField] ParticleSystem smokeParticles; //Smoke particles reference
    [SerializeField] AudioSource audioSource;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Grenade")) return;

        audioSource.Play();
        smokeParticles.Play();
            Destroy(gameObject, 10);

    }
}
