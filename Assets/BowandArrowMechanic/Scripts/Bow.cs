using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;    //The arrow prefab gameobject we are instantiating
    [SerializeField] Transform arrowLocation;   //The location to place the arrow
    [SerializeField] float arrowThrowForce;     //How hard to throw the arrow
    [SerializeField] int arrowCount;    //How many arrows we have
    [SerializeField] GameObject newArrow;   //Current arrow gameobject reference, filled when we spawn a new arrow
    [SerializeField] float arrowReloadSpeed; //How fast can we reload an arrow?

    public int ArrowCount { get => arrowCount; set => arrowCount = value; } //Property for arrow count

    // Start is called before the first frame update
    void Start()
    {
        //If arrow count is greater than 0 place a new arrow for us to shoot
        if(ArrowCount > 0)
        PlaceNewArrow();
    }

    // Update is called once per frame
    void Update()
    {
        //If we press right mouse button, fire our currently placed arrow
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireArrow();
        }
    }

    //Places a new arrow
     void PlaceNewArrow()
    {
        //Decreases arrow count
        ArrowCount--;

        //Create a new arrow at the arrow location and rotation, and parent it to the arrow location, which is within the bow.
        newArrow = Instantiate(arrowPrefab, arrowLocation.position, arrowLocation.rotation);
        newArrow.transform.parent = arrowLocation;
    }

    //Fire our current arrow
     void FireArrow()
    {
        //If our newarrow gameobject is null, we have no arrow, go away
        if (newArrow == null) return;

        //Else, Get the arrow component/boxcollidertrigger and enable it, set rigidbody iskinematic to false
        newArrow.GetComponent<Arrow>().boxColliderTrigger.enabled = true;
        newArrow.GetComponent<Rigidbody>().isKinematic = false;

        //Get the rigidbody of the new arrow and add force at the arrow location.forward times arrow throw force, then null out the parent and new arrow objects because we are done with this arrow
        newArrow.GetComponent<Rigidbody>().AddForce(arrowLocation.forward * arrowThrowForce, ForceMode.Impulse);
        newArrow.transform.parent = null;
        newArrow = null;

        //If arrowcount is still above arrowcount, call place a new arrow after 2 seconds
        if (ArrowCount > 0)
            Invoke("PlaceNewArrow", arrowReloadSpeed);
    }
}
