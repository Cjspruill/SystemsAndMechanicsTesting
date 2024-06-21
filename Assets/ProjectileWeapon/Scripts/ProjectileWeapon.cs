using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    [SerializeField] float fireRate; //How fast can we fire?
    [SerializeField] float fireRateTimer; //Fire rate timer
    [SerializeField] GameObject projectilePrefab; //Projectile prefab reference we will be spawning
    [SerializeField] float firePower; //How hard should we fire the projectile?
    [SerializeField] Transform barrelOutLocation; //Where we place the projectile after spawning
    [SerializeField] float ammo; //How much ammo we have

    public float Ammo { get => ammo; set => ammo = value; } //Property for ammo

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Increase fire rate by time.deltatime
        fireRateTimer += Time.deltaTime;

        //If fire rate is greater or equal to fire rate
        if(fireRateTimer >= fireRate)
        {
            //If we press left mouse button, fire the weapon
            if (Input.GetKeyDown(KeyCode.Mouse0) && Ammo > 0)
            {
                FireWeapon();
            }
        }
    }

    void FireWeapon()
    {
        Ammo--;
        //Create the new projectile at the barrel out location, then add a force to it to propel it forward
        GameObject newProjectile = Instantiate(projectilePrefab, barrelOutLocation.position, barrelOutLocation.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(barrelOutLocation.forward * firePower, ForceMode.Impulse);

        //Reset fire rate timer
        fireRateTimer = 0;
    }
}
