using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] Transform canonBarrelOutLocation;
    [SerializeField] GameObject canonBallPrefab;
    [SerializeField] float canonBallFirePower;
    [SerializeField] float fuseTime;

    [SerializeField] bool isInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LightFuse();
            }

        }
    }

    void LightFuse()
    {
        //Start invoke or coroutine for firecanon
        Invoke("FireCanon", fuseTime);
    }

    void FireCanon()
    {
        GameObject newCanonBall = Instantiate(canonBallPrefab, canonBarrelOutLocation.position, canonBarrelOutLocation.rotation);
        newCanonBall.GetComponent<Rigidbody>().AddForce(canonBarrelOutLocation.forward * canonBallFirePower, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }
}
