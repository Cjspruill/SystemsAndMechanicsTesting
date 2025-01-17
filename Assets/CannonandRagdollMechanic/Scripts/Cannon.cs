using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform cannonBarrelOutLocation;  //Cannon ball spawn location
    [SerializeField] GameObject cannonBallPrefab;    //Cannon ball gameobject prefab we are spawning
    [SerializeField] float cannonBallFirePower;  //How hard are we firing the cannonball?
    [SerializeField] float fuseTime;    //How long should the fuse burn before it fires?
    [SerializeField] bool isInTrigger;  //Set if the player is in the trigger area
    [SerializeField] GameObject muzzleFlash; //Muzzle flash particle system reference, we are just turning it on and off

    // Update is called once per frame
    void Update()
    {
        //If is in trigger, and user presses e key, light the fuse
        if (isInTrigger)     
            if (Input.GetKeyDown(KeyCode.E))         
                LightFuse(); 
    }

    //Lights the canon fuse
    void LightFuse()
    {
        //Start invoke for firecannon
        Invoke("FireCanon", fuseTime);
    }

    //Fires a cannonball
    void FireCanon()
    {
        //Creates a new cannonball and launches it with rigidbody.addforce
        GameObject newCanonBall = Instantiate(cannonBallPrefab, cannonBarrelOutLocation.position, cannonBarrelOutLocation.rotation);
        newCanonBall.GetComponent<Rigidbody>().AddForce(cannonBarrelOutLocation.forward * cannonBallFirePower, ForceMode.Impulse);
        muzzleFlash.GetComponent<ParticleSystem>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If other tag is player, is in trigger is true
        if (other.CompareTag("Player"))
            isInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //If other tag is player, is in trigger is false
        if (other.CompareTag("Player"))
            isInTrigger = false;
    }
}
