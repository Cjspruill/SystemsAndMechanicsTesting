using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] float checkDistance;

    [SerializeField] LayerMask layerMask;
    [SerializeField] float repeatRate;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckForBrick", 0, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void CheckForBrick()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, checkDistance, layerMask))
        {
            if (hit.collider.GetComponent<Rigidbody>() != null)
            {
                Debug.Log("Brick under us");
            }
            else
            {
            }
        }
        else
        {
            Debug.Log("Falling");
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }








    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.down) * checkDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
