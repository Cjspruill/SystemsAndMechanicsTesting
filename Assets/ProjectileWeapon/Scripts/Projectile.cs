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
        if (collision.collider.GetComponentInParent<Overshield>() != null)
        {
            collision.collider.GetComponentInParent<Overshield>().TakeShieldDamage(damage);
        }
        //If a health component exist
        else if (collision.collider.GetComponentInParent<Health>() != null)
        {
            //If we find the player and he is blocking, deal half damage
            if (collision.collider.CompareTag("Player") && collision.collider.GetComponentInParent<Shield>().IsBlocking)
            {
                collision.collider.GetComponentInParent<Health>().TakeDamage(damage / 2);
                Destroy(gameObject);
            }
            //Else deal full damage
            else
            {
                //Take damage
                collision.collider.GetComponentInParent<Health>().TakeDamage(damage);
                //Destroy gameobject
                Destroy(gameObject);
            }
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
