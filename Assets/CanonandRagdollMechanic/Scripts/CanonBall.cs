using UnityEngine;

public class CanonBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().enabled = true;
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent < Rigidbody>() != null)
        {
            collision.collider.GetComponent<Rigidbody>().isKinematic = false;

            if (collision.collider.GetComponentInParent<RagdollController>() != null)
            {
                collision.collider.GetComponentInParent<RagdollController>().SetRagdollActive();
            }
        }
    }
}
