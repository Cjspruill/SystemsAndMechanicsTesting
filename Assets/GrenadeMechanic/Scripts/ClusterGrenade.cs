using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterGrenade : MonoBehaviour
{
    [SerializeField] float cookTime; //How long before this blows up?
    [SerializeField] ParticleSystem smokeParticles; //Smoke particles reference
    [SerializeField] ClusterBomb clusterBombPrefab;
    [SerializeField] int clusterSize;
    [SerializeField] float clusterExplosionForce;
    [SerializeField] SphereCollider sphereCollider;
    public void ThrowGrenade()
    {
        Invoke("Explode", cookTime);
    }

    void Explode()
    {
        smokeParticles.Play();

        GetComponent<MeshRenderer>().enabled = false;
        sphereCollider.enabled = false;
        for (int i = 0; i < clusterSize; i++)
        {
            Vector3 newSpawnPos = new Vector3(Random.Range(transform.position.x - .1f, transform.position.x + .1f), transform.position.y, Random.Range(transform.position.z - .1f, transform.position.z + .1f));
            ClusterBomb newGrenade = Instantiate(clusterBombPrefab, newSpawnPos, transform.rotation);
            newGrenade.GetComponent<Rigidbody>().AddExplosionForce(clusterExplosionForce, transform.position, 1);
        }
        Destroy(gameObject, 10f);
    }
}
