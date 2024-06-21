using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{ 
    [SerializeField] public BoxCollider boxColliderTrigger;  //BoxColliderTrigger, turned on or off from bow
    [SerializeField] bool isOnFire; //Is the arrow on fire?
    [SerializeField] GameObject fireParticles; //Reference to the particle system stored as a game object

    public bool IsOnFire { get => isOnFire; set => isOnFire = value; }  //Property for is on fire

    private void OnCollisionEnter(Collision collision)
    {
        //If i collide with a wall
       if(collision.collider.CompareTag("Wall"))
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
        //If i collide with a player
        if (other.CompareTag("Player"))
        {
            //If the player has a bow component, add to arrow count
            if (other.GetComponent<Bow>() != null)
            {
                other.GetComponent<Bow>().ArrowCount++;

                if (other.GetComponent<Bow>().NewArrow == null && other.GetComponent<Bow>().ArrowCount >0)
                {
                    other.GetComponent<Bow>().PlaceNewArrow();
                }
            }
            //If the player has a bow version 2 add to arrow count
            else if (other.GetComponent<BowVersion2>() != null)
            {
                other.GetComponent<BowVersion2>().ArrowCount++;

                if (other.GetComponent<BowVersion2>().NewArrow == null && other.GetComponent<BowVersion2>().ArrowCount > 0)
                {
                    other.GetComponent<BowVersion2>().PlaceNewArrow();
                }
            }
            else if (other.GetComponent<BowVersion3>() != null)
            {
                other.GetComponent<BowVersion3>().ArrowCount++;

                if (other.GetComponent<BowVersion3>().NewArrow == null && other.GetComponent<BowVersion3>().ArrowCount > 0)
                {
                    other.GetComponent<BowVersion3>().PlaceNewArrow();
                }
            }
            //Destroy the arrow you collided with
            Destroy(gameObject);
        }

        if (other.CompareTag("Fire"))
        {
            IsOnFire = true;
            fireParticles.GetComponent<ParticleSystem>().Play();
        }
    }
}
