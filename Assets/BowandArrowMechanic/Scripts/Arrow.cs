using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]public BoxCollider boxColliderTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.collider.CompareTag("Wall"))
        {
            transform.position = transform.position;
            transform.rotation = transform.rotation;
            transform.parent = collision.collider.transform;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Bow>() != null)
                other.GetComponent<Bow>().ArrowCount++;
            else if (other.GetComponent<BowVersion2>() != null)
                other.GetComponent<BowVersion2>().ArrowCount++;
            //if(other.GetComponent<Bow>().ArrowCount==1)
            Destroy(gameObject);
        }

    }
}
