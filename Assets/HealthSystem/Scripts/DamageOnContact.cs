using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] float damage;

    [SerializeField] bool damageByCollider;
    [SerializeField] bool hasDoneDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != null && !hasDoneDamage)
        {
            other.GetComponent<Health>().TakeDamage(damage);
            hasDoneDamage = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (damageByCollider)
        {
            if (collision.collider.GetComponent<Health>() != null &&!hasDoneDamage)
            {
                collision.collider.GetComponent<Health>().TakeDamage(damage);
                hasDoneDamage = true;
            }
        }
    }
}
