using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDurability : MonoBehaviour
{
    [SerializeField] WeaponDurabilityController weaponDurabilityController;
    [SerializeField] public bool alreadyHit;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;
        if (other.CompareTag("Wall") || other.CompareTag("Metal") || other.CompareTag("Concrete") || other.CompareTag("Brick"))
        {
            if (!weaponDurabilityController.IsSwinging) return;
            alreadyHit = true;
            weaponDurabilityController.ReduceDurability(10);
            alreadyHit = false;
        }
    }
}
