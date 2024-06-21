using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float destroyTime; //How long until it destroys itself
    [SerializeField] float damage; //How much damage does this deal?
    private void Start()
    {
        GetComponent<SphereCollider>().enabled = true;
        //Destroy on start
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If a health component exist
        if (collision.collider.GetComponentInParent<Health>() != null)
        {
            //Take damage
            collision.collider.GetComponentInParent<Health>().TakeDamage(damage);
            //Destroy gameobject
            Destroy(gameObject);
        }

        //If a color change component exist
        if (collision.collider.GetComponentInParent<ColorChangeOnCollision>() != null)
        {
            //Change color
            collision.collider.GetComponentInParent<ColorChangeOnCollision>().ChangeColor();
        //Destroy gameobject
        Destroy(gameObject);
        }
    }
}
