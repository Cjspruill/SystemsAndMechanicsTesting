using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] float damage;  //The amount of damage to deal
    [SerializeField] bool damageByCollider; //Does this deal damage by collider or trigger?
    [SerializeField] bool hasDoneDamage;    //Has this object dealt damage once already?

    private void OnTriggerEnter(Collider other)
    {
        //If other object has a health component and we havent dealt damage yet, deal damage and set donedamage to true
        if (other.GetComponent<Health>() != null && !hasDoneDamage)
        {
            other.GetComponent<Health>().TakeDamage(damage);
            hasDoneDamage = true;
        }
        //Check in parent for health component if above is false
        else if(other.GetComponentInParent<Health>()!=null && !hasDoneDamage)
        {
            other.GetComponentInParent<Health>().TakeDamage(damage);
            hasDoneDamage = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If damage by collider is true
        if (damageByCollider)
        {
            //If we hit a health component and havent done damage yet, deal damage and set has done damage to true
            if (collision.collider.GetComponent<Health>() != null &&!hasDoneDamage)
            {
                collision.collider.GetComponent<Health>().TakeDamage(damage);
                hasDoneDamage = true;
            }
            //Check if health in parent is not null if above is
            else if (collision.collider.GetComponentInParent<Health>() != null && !hasDoneDamage)
            {
                collision.collider.GetComponentInParent<Health>().TakeDamage(damage);
                hasDoneDamage = true;
            }
        }
    }
}
