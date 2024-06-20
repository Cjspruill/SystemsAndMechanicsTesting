using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{ 
    [SerializeField] public BoxCollider boxColliderTrigger;  //BoxColliderTrigger, turned on or off from bow

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
                other.GetComponent<Bow>().ArrowCount++;
            //If the player has a bow version 2 add to arrow count
            else if (other.GetComponent<BowVersion2>() != null)
                other.GetComponent<BowVersion2>().ArrowCount++;
            //if(other.GetComponent<Bow>().ArrowCount==1)

            //Destroy the arrow you collided with
            Destroy(gameObject);
        }
    }
}
