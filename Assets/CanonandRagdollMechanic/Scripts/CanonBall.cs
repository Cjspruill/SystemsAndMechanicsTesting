using UnityEngine;

public class CanonBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Gets the sphere collider of the canonball and turns it on, then calls destroy gameobject after 10 seconds
        GetComponent<SphereCollider>().enabled = true;
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the collider i hit has a rigidbody, set is kinematic to false
        if (collision.collider.GetComponent<Rigidbody>() != null)
        {
            collision.collider.GetComponent<Rigidbody>().isKinematic = false;

            //If the collider i hit now has a ragdoll controller in the parent, set ragdoll to active
            if (collision.collider.GetComponentInParent<RagdollController>() != null)
            {
                collision.collider.GetComponentInParent<RagdollController>().SetRagdollActive();
            }
        }
    }
}
