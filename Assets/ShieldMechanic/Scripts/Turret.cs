using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]public enum EnemyState    //Enemy state enum holding all the states
    {
        Idle,
        Notice,
        Track,
        Down
    }
    [SerializeField]public EnemyState enemyState; //Holds current enemy state
    [SerializeField] Transform barrelOut; //The location of where to spawn the projectile
    [SerializeField] GameObject projectilePrefab;   //The reference to the projectile prefab
    [SerializeField] float projectileSpeed; // How fast do we throw the projectile?
    [SerializeField] float rotationSpeed;   //How fast we rotate;
    [SerializeField] GameObject player; //Player reference to track
    [SerializeField] MeshRenderer[] meshRenderers; //List of my mesh renderers
    [SerializeField] Color origColor;   //The original color of the object
    public GameObject Player { get => player; set => player = value; } //Player property
    [SerializeField] GameObject turret; //The turret itself we will be rotating;
    [SerializeField] float fireRate; //Firerate of the turret
    [SerializeField] float fireRateTimer; //Timer for fire rate
    [SerializeField] GameObject muzzleFlash; //Muzzleflash reference

    [SerializeField] float downTime;
    [SerializeField] float downTimer;

    [SerializeField] AINoticeRange aiNoticeRange;
    [SerializeField] AIChaseRange aiChaseRange;
    [SerializeField] ParticleSystem sparksParticles;

    // Start is called before the first frame update
    void Start()
    {
        origColor = meshRenderers[0].material.color;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    meshRenderers[i].material.color = origColor;
                }
                break;
            case EnemyState.Notice:
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    meshRenderers[i].material.color = Color.yellow;
                }

                if (player != null)
                {
                    //Track player
                    Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - turret.transform.position);
                    turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
                }
                break;
            case EnemyState.Track:

                //Increase fire rate timer by time.delta time
                fireRateTimer += Time.deltaTime;

                //Set mesh renderers to red
                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    meshRenderers[i].material.color = Color.red;
                }

                //If player is not null
                if (player != null)
                {
                //Track player
                Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - turret.transform.position);
                turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
                }

                if(fireRateTimer>= fireRate)
                {
                    fireRateTimer = 0;
                    FireTurret();
                }
                break;

            case EnemyState.Down:

                downTimer += Time.deltaTime;

                sparksParticles.Play();
                //Set turret rotation to down position
                Quaternion newRot = Quaternion.Euler(45, turret.transform.rotation.y, turret.transform.rotation.z);
                turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, newRot, rotationSpeed * Time.deltaTime);

                for (int i = 0; i < meshRenderers.Length; i++)
                {
                    meshRenderers[i].material.color = Color.magenta;
                }

                if(downTimer >= downTime)
                {
                    downTimer = 0;
                    if (aiChaseRange.isActive)
                        enemyState = EnemyState.Track;
                    else if (aiNoticeRange.isActive)
                        enemyState = EnemyState.Notice;
                    else                  
                        enemyState = EnemyState.Idle;
                    
                    sparksParticles.Stop();
                }

                break;
            default:
                break;
        }
    }

    void FireTurret()
    {
        muzzleFlash.GetComponent<ParticleSystem>().Play();
        GameObject newProjectile = Instantiate(projectilePrefab, barrelOut.position, barrelOut.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(barrelOut.forward * projectileSpeed, ForceMode.Impulse);
    }

    public void SetEnemyStateToDown()
    {
        enemyState = EnemyState.Down;
    }
}
