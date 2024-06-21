using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowVersion3 : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;    //Our arrow prefab gameobject
    [SerializeField] Transform arrowLocation;   //The location of where we will place our arrows
    [SerializeField] float arrowThrowForce; //How hard do we throw these arrows?
    [SerializeField] int arrowCount;    //How many arrows do we have?
    [SerializeField] GameObject newArrow;   //New arrow reference, used to do things with the arrow before it is fired.
    [SerializeField] float arrowReloadSpeed;    //How fast can we reload?
    public int ArrowCount { get => arrowCount; set => arrowCount = value; } //Property for arrow count
    public GameObject NewArrow { get => newArrow; set => newArrow = value; } //Property for new arrow
    [SerializeField] Animator bowAnimator; //Animator for the bow object

    // Start is called before the first frame update
    void Start()
    {
        //If arrowcount is greater than 0, place a new arrow
        if (ArrowCount > 0)
            PlaceNewArrow();
    }
    // Update is called once per frame
    void Update()
    {

        //If we hold down right mouse button, ready our bow
        if (Input.GetKey(KeyCode.Mouse1))
        {
           //Animator.setbool ReadyBow();
        }
        //If we release right mouse button, unready our bow
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
           //Animator.setbool UnreadyBow();
        }
    }

    //Places a new arrow
    public void PlaceNewArrow()
    {
        //Decrement arrow count
        ArrowCount--;

        //Create a new arrow and parent it to the arrow location
        NewArrow = Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation);
        NewArrow.transform.parent = arrowLocation;
    }

    //Fires our currently loaded arrow
    void FireArrow()
    {
        //If new arrow is null go away
        if (NewArrow == null) return;

        //Turn on the boxcollidertrigger of the arrow, and set the rigidbody iskinematic to isfalse

        NewArrow.GetComponent<Rigidbody>().isKinematic = false;

        //Add force to the rigidbody, set the parent to null
        NewArrow.GetComponent<Rigidbody>().AddForce(arrowLocation.forward * arrowThrowForce, ForceMode.Impulse);
        NewArrow.transform.parent = null;
        Invoke("EnableBoxCollider", .15f);
        //If we still have arrows, place a new arrow, wait for reload speed
        if (ArrowCount > 0)
            Invoke("PlaceNewArrow", arrowReloadSpeed);
    }
    void EnableBoxCollider()
    {
        NewArrow.GetComponent<Arrow>().boxColliderTrigger.enabled = true;
        NewArrow = null;
    }
}