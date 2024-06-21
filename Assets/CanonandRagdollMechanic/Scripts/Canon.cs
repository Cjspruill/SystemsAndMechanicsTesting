using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] Transform canonBarrelOutLocation;  //Canon ball spawn location
    [SerializeField] GameObject canonBallPrefab;    //Canon ball gameobject prefab we are spawning
    [SerializeField] float canonBallFirePower;  //How hard are we firing the canonball?
    [SerializeField] float fuseTime;    //How long should the fuse burn before it fires?
    [SerializeField] bool isInTrigger;  //Set if the player is in the trigger area

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
        //Start invoke for firecanon
        Invoke("FireCanon", fuseTime);
    }

    //Fires a canonball
    void FireCanon()
    {
        //Creates a new canonball and launches it with rigidbody.addforce
        GameObject newCanonBall = Instantiate(canonBallPrefab, canonBarrelOutLocation.position, canonBarrelOutLocation.rotation);
        newCanonBall.GetComponent<Rigidbody>().AddForce(canonBarrelOutLocation.forward * canonBallFirePower, ForceMode.Impulse);
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
