using UnityEngine;

public class CannonBall : MonoBehaviour
{

    [SerializeField] float power; //How much force does this do on impact?

    // Start is called before the first frame update
    void Start()
    {
        //Gets the sphere collider of the canonball and turns it on, then calls destroy gameobject after 10 seconds
        GetComponent<SphereCollider>().enabled = true;
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player")) return;

        //If the collider i hit has a rigidbody, set is kinematic to false
        if (collision.collider.GetComponent<Rigidbody>() != null)
        {
            collision.collider.GetComponent<Rigidbody>().isKinematic = false;


            //If the collider i hit now has a ragdoll controller in the parent, set ragdoll to active
            if (collision.collider.GetComponentInParent<RagdollController>() != null)
            {
                if (collision.collider.GetComponentInParent<Titan>() != null)
                {
                    if(collision.collider.GetComponentInParent<Titan>().Health <= 0)
                    {
                        collision.collider.GetComponentInParent<RagdollController>().SetRagdollActive();
                    }
                }
                else
                {

                collision.collider.GetComponentInParent<RagdollController>().SetRagdollActive();
                }
            }
            collision.collider.GetComponent<Rigidbody>().AddForce(transform.forward * power, ForceMode.Impulse);
        }
    }
}
