using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableCannon : MonoBehaviour
{
    [SerializeField] Transform cannonBarrelOutLocation;  //Cannon ball spawn location
    [SerializeField] GameObject cannonBallPrefab;    //Cannon ball gameobject prefab we are spawning
    [SerializeField] float cannonBallFirePower;  //How hard are we firing the cannonball?
    [SerializeField] float fuseTime;    //How long should the fuse burn before it fires?
    [SerializeField] GameObject muzzleFlash; //Muzzle flash particle system reference, we are just turning it on and off
    [SerializeField] ParticleSystem sparks; //Spark particle system
    [SerializeField] float fireRate;
    [SerializeField] float fireRateTimer;

    // Update is called once per frame
    void Update()
    {
        fireRateTimer += Time.deltaTime;

        if(fireRateTimer>= fireRate)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                LightFuse();

        }
    }

    //Lights the canon fuse
    void LightFuse()
    {
        fireRateTimer = 0;
        sparks.Play();
        //Start invoke for firecannon
        Invoke("FireCanon", fuseTime);
    }

    //Fires a cannonball
    void FireCanon()
    {
        sparks.Stop();
        //Creates a new cannonball and launches it with rigidbody.addforce
        GameObject newCanonBall = Instantiate(cannonBallPrefab, cannonBarrelOutLocation.position, cannonBarrelOutLocation.rotation);
        newCanonBall.GetComponent<Rigidbody>().AddForce(cannonBarrelOutLocation.forward * cannonBallFirePower, ForceMode.Impulse);
        muzzleFlash.GetComponent<ParticleSystem>().Play();
    }

}
