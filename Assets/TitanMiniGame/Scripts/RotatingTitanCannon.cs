using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTitanCannon : MonoBehaviour
{
    [SerializeField] Transform cannonBarrelOutLocation;  //Cannon ball spawn location
    [SerializeField] GameObject cannonBallPrefab;    //Cannon ball gameobject prefab we are spawning
    [SerializeField] float cannonBallFirePower;  //How hard are we firing the cannonball?
    [SerializeField] float fuseTime;    //How long should the fuse burn before it fires?
    [SerializeField] GameObject muzzleFlash; //Muzzle flash particle system reference, we are just turning it on and off
    [SerializeField] float fireTimer;
    [SerializeField] float fireTime;
    [SerializeField] bool isInTrigger;

    [SerializeField] float rotationStepX; //Rotation increment for left and right
    [SerializeField] float rotationStepY; //Rotation increment for up and down
    [SerializeField] bool isLoaded;
    [SerializeField] ParticleSystem sparksParticles;
    public float CannonBallFirePower { get => cannonBallFirePower; set => cannonBallFirePower = value; }
    public bool IsLoaded { get => isLoaded; set => isLoaded = value; }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IsLoaded = true;
            }
        }

        if (Input.GetKey(KeyCode.I))
        {
            RotateCannonLeftRight(true);
        }
        if (Input.GetKey(KeyCode.K))
        {
            RotateCannonLeftRight(false);
        }
        if(Input.GetKey(KeyCode.L))
        {
            RotateCannonUpDown(true);
        }
        if (Input.GetKey(KeyCode.J))
        {
            RotateCannonUpDown(false);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            DecrementFirePower();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            IncrementFirePower();
        }

    }

    //Lights the canon fuse
    public void LightFuse()
    {
        if (fireTimer >= fireTime)
        {
            if (IsLoaded)
            {
                sparksParticles.Play();
        //Start invoke for firecannon
        Invoke("FireCanon", fuseTime);
            fireTimer = 0;
            }
        }
    }

    //Fires a cannonball
    void FireCanon()
    {
        sparksParticles.Stop();
        //Creates a new cannonball and launches it with rigidbody.addforce
        GameObject newCanonBall = Instantiate(cannonBallPrefab, cannonBarrelOutLocation.position, cannonBarrelOutLocation.rotation);
        newCanonBall.GetComponent<Rigidbody>().AddForce(cannonBarrelOutLocation.forward * CannonBallFirePower, ForceMode.Impulse);
        muzzleFlash.GetComponent<ParticleSystem>().Play();
        IsLoaded = false;
    }

    public void RotateCannonLeftRight(bool left)
    {
        if (left)
            rotationStepX -= 1f;
        else
            rotationStepX += 1f;

        transform.rotation = Quaternion.Euler(transform.rotation.x + rotationStepX, transform.rotation.y + rotationStepY, transform.rotation.z);
    }

    public void RotateCannonUpDown(bool up)
    {
        if (up)
            rotationStepY += 1f;
        else
            rotationStepY -= 1f;

        transform.rotation = Quaternion.Euler(transform.rotation.x + rotationStepX, transform.rotation.y + rotationStepY, transform.rotation.z);
    }

    public void IncrementFirePower()
    {
        CannonBallFirePower += 500;
    }

    public void DecrementFirePower()
    {
        CannonBallFirePower -= 500;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isInTrigger = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isInTrigger = false;
    }
}
