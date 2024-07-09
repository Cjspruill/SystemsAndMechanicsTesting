using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GrappleController : MonoBehaviour
{

    [SerializeField] Transform grappleHookFireLocation;
    [SerializeField] Vector3 grapplePoint;
    [SerializeField] float distance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float grappleTime;
    [SerializeField] float grappleTimer;
    [SerializeField] float grappleWindSpeed;
    [SerializeField] bool isGrappling;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grappleTimer += Time.deltaTime;

        if (grappleTimer >= grappleTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FireGrappleHook();
                grappleTimer = 0;
            }

        }


        if (isGrappling)
        {
            transform.position = Vector3.MoveTowards(transform.position, grapplePoint, grappleWindSpeed * Time.deltaTime);
        }
    }

    void FireGrappleHook()
    {
        RaycastHit hit;
        if(Physics.Raycast(grappleHookFireLocation.position,grappleHookFireLocation.forward,out hit, distance, layerMask))
        {
            grapplePoint = hit.point;
            GetComponent<FirstPersonController>().enabled = false;
            isGrappling = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isGrappling)
        {
        isGrappling = false;
            GetComponent<FirstPersonController>().enabled = true;
        }
    }
}
