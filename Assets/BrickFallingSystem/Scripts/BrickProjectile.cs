using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickProjectile : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] GameObject player;
    [SerializeField] bool alreadyDidForce;
    [SerializeField] bool isArrowVersion3;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        if(GetComponent<SphereCollider>()!=null)
        GetComponent<SphereCollider>().enabled = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Rigidbody>() != null)
        {
            if (other.collider.CompareTag("Player")) return;

            if (alreadyDidForce) return;
            other.collider.GetComponent<Rigidbody>().isKinematic = false;
            if (isArrowVersion3)
            {
                other.collider.GetComponent<Rigidbody>().AddForce(transform.right * force, ForceMode.Impulse);
                audioSource.Play();
            }
            else
                other.collider.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            if (other.collider.GetComponent<Brick>() != null)
                other.collider.GetComponent<Brick>().StopAllCoroutines();
            alreadyDidForce = true;

            
        }
    }
}
