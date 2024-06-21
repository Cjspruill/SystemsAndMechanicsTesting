using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangPickup : MonoBehaviour
{
    [SerializeField] BoomerangVersion2 boomerangVersion2; //Reference to boomerang version 2 script on player

    private void OnTriggerStay(Collider other)
    {
        //If other tag is player
        if (other.CompareTag("Player"))
        {
            //If player has a boomerang version 2
            if (other.GetComponent<BoomerangVersion2>() != null)
            {
                //If we press e key, pick up boomerang
                if(Input.GetKeyDown(KeyCode.E))
                    other.GetComponent<BoomerangVersion2>().PickupBoomerang(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (boomerangVersion2.Boomerang != null)
        {
            //Turn off is kinematic, reenable box collider so we can pick it up, and disable the rotator so it stops spinning
            boomerangVersion2.Boomerang.GetComponent<Rigidbody>().isKinematic = false;
            boomerangVersion2.Boomerang.GetComponent<BoxCollider>().enabled = true;
            boomerangVersion2.Boomerang.GetComponent<Rotator>().enabled = false;
            boomerangVersion2.Boomerang = null;
        }
    }
}
