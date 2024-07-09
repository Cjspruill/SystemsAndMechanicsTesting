using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    [SerializeField] Grenade grenadePrefab;
    [SerializeField] StickyGrenade stickyGrenadePrefab;
    [SerializeField] TeleportGrenade teleportGrenadePrefab;
    [SerializeField] SmokeGrenade smokeGrenadePrefab;
    [SerializeField] ClusterGrenade clusterGrenadePrefab;
    [SerializeField] Grenade bouncingBettyGrenadePrefab;
    [SerializeField] MagnetGrenade magnetGrenadePrefab;
    [SerializeField] EmpGrenade empGrenadePrefab;
    [SerializeField] ProximityMine proximityMinePrefab;
    [SerializeField] FlashBangGrenade flashBangGrenadePrefab;
    [SerializeField] BlackHoleGrenade blackHoleGrenadePrefab;
    [SerializeField] Transform grenadeThrowLocation;
    [SerializeField] float throwPower;
    [SerializeField] float throwRate;
    [SerializeField] float throwRateTimer;

    [SerializeField] public enum GrendadeType
    {
        Frag,
        Sticky,
        Teleport,
        Smoke,
        Cluster,
        BouncingBetty,
        Magnet,
        Emp,
        ProximityMine,
        FlashBang,
        BlackHole,
    }

    [SerializeField] public GrendadeType grenadeType;
    [SerializeField] int grenadeIndex;


    // Start is called before the first frame update
    void Start()
    {
        grenadeType = GrendadeType.Frag;
    }

    // Update is called once per frame
    void Update()
    {
        throwRateTimer += Time.deltaTime;

        if (throwRateTimer >= throwRate)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                throwRateTimer = 0;
                SpawnNewGrenade(grenadeType);
            }
        }


        //Scroll down
        if (Input.GetAxis("Mouse ScrollWheel") > .1)
        {
            DecreaseIndex();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -.1)
        {
            IncreaseIndex();
        }

    }

    void SpawnNewGrenade(GrendadeType grenadeType)
    {
        if(grenadeType == GrendadeType.Frag)
        {
            GameObject newGrenade = Instantiate(grenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<Grenade>().ThrowGrenade();
        }
        if (grenadeType == GrendadeType.Sticky)
        {
            GameObject newGrenade = Instantiate(stickyGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<StickyGrenade>().ThrowGrenade();
        }
        if (grenadeType == GrendadeType.Teleport)
        {
            GameObject newGrenade = Instantiate(teleportGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
        }
        if (grenadeType == GrendadeType.Smoke)
        {
            GameObject newGrenade = Instantiate(smokeGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
        }
        if (grenadeType == GrendadeType.Cluster)
        {
            GameObject newGrenade = Instantiate(clusterGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<ClusterGrenade>().ThrowGrenade();
        }
        if (grenadeType == GrendadeType.BouncingBetty)
        {
            GameObject newGrenade = Instantiate(bouncingBettyGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<Grenade>().ThrowGrenade();
        }
        if (grenadeType == GrendadeType.Magnet)
        {
            GameObject newGrenade = Instantiate(magnetGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<MagnetGrenade>().ThrowGrenade();
        }
        if (grenadeType == GrendadeType.Emp)
        {
            GameObject newGrenade = Instantiate(empGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<EmpGrenade>().ThrowGrenade();
        }
        if (grenadeType == GrendadeType.ProximityMine)
        {
            GameObject newGrenade = Instantiate(proximityMinePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
        }
        if (grenadeType == GrendadeType.FlashBang)
        {
            GameObject newGrenade = Instantiate(flashBangGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<FlashBangGrenade>().ThrowGrenade();
        }
        if (grenadeType == GrendadeType.BlackHole)
        {
            GameObject newGrenade = Instantiate(blackHoleGrenadePrefab.gameObject, grenadeThrowLocation.position, grenadeThrowLocation.rotation);
            newGrenade.GetComponent<Rigidbody>().AddForce(grenadeThrowLocation.forward * throwPower, ForceMode.Impulse);
            newGrenade.GetComponent<BlackHoleGrenade>().ThrowGrenade();
        }
    }

    void IncreaseIndex()
    {
        grenadeIndex++;

        if(grenadeIndex > (int)GrendadeType.BlackHole)
        {
            grenadeIndex = 0;
        }

        CheckGrenadeType();
    }

    void DecreaseIndex()
    {
        grenadeIndex--;

        if(grenadeIndex < 0)
        {
            grenadeIndex = (int)GrendadeType.BlackHole;
        }

        CheckGrenadeType();
    }

    void CheckGrenadeType()
    {
        switch (grenadeIndex)
        {
            case 0:
                grenadeType = GrendadeType.Frag;
                break;
            case 1:
                grenadeType = GrendadeType.Sticky;
                break;
            case 2:
                grenadeType = GrendadeType.Teleport;
                break;
            case 3:
                grenadeType = GrendadeType.Smoke;
                break;
            case 4:
                grenadeType = GrendadeType.Cluster;
                break;
            case 5:
                grenadeType = GrendadeType.BouncingBetty;
                break;
            case 6:
                grenadeType = GrendadeType.Magnet;
                break;
            case 7:
                grenadeType = GrendadeType.Emp;
                break;
            case 8:
                grenadeType = GrendadeType.ProximityMine;
                break;
            case 9:
                grenadeType = GrendadeType.FlashBang;
                break;
            case 10:
                grenadeType = GrendadeType.BlackHole;
                break;
        }
    }
}
