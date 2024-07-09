using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickVersion2 : MonoBehaviour
{
    [SerializeField] GameObject supportBrick;
    [SerializeField] float checkDistance;
    [SerializeField] LayerMask layerMask;

    public GameObject SupportBrick { get => supportBrick; set => supportBrick = value; }

    private void Start()
    {
            RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, checkDistance, layerMask))
        {
            if (hit.collider.CompareTag("Brick") || layerMask.ToString() == "Environment")
            {
                SupportBrick = hit.collider.gameObject;
            }
        }
    }

    private void Update()
    {
        if (SupportBrick == null)
        {
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
