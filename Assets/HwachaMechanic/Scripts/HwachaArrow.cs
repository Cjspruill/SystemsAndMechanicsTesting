using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwachaArrow : MonoBehaviour
{
    [SerializeField] bool isOnFire; //Is the arrow on fire?
    [SerializeField] GameObject fireParticles; //Reference to the particle system stored as a game object
    [SerializeField] float fuseTime;
    [SerializeField] float fuseTimerMin;
    [SerializeField] float fuseTimerMax;
    [SerializeField] float fireForce;
    [SerializeField] float fireForceMin;
    [SerializeField] float fireForceMax;

    public bool IsOnFire { get => isOnFire; set => isOnFire = value; }  //Property for is on fire

    private void OnCollisionEnter(Collision collision)
    {
        //If i collide with a wall
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Ground"))
        {
            //Set position and rotation to collided position
            transform.position = transform.position;
            transform.rotation = transform.rotation;
            //Set parent and turn on is kinematic in rigidbody
            transform.parent = collision.collider.transform;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            IsOnFire = true;
            fireParticles.GetComponent<ParticleSystem>().Play();
        }
    }

    public void LightFuse()
    {
        fuseTime = Random.Range(fuseTimerMin, fuseTimerMax);
        Invoke("FireArrow", fuseTime);
    }

    public void FireArrow()
    {
        fireForce = Random.Range(fireForceMin, fireForceMax);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * fireForce, ForceMode.Impulse);
        GetComponent<BoxCollider>().enabled = true;
    }

}
