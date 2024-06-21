using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangVersion2 : MonoBehaviour
{
    [SerializeField] GameObject boomerang; //Game object reference to boomerang. Used to move boomerang
    [SerializeField] Transform boomerangLocation; //Original boomerang location
    [SerializeField] Transform boomerangRotation; //Original boomerang rotation
    [SerializeField] float boomerangDistance; //How far can we throw this thing
    [SerializeField] float throwSpeed; //Throw speed of this thing
    [SerializeField] float throwPower; //How hard are we throwing this thing with the rigidbody?
    [SerializeField] LayerMask layerMask; //Layer mask for raycast check. Looking for environment layer

    [SerializeField] bool isThrown; //Bool that gets set when thrown
    [SerializeField] bool isReturning; //Bool that get set after hitting the middle point

    [SerializeField] Vector3 throwPosition; //This is where the boomerang is traveling to.
    [SerializeField] Rotator rotator; //Rotator on boomerang object. Gets turned on when thrown. And off when not.

    [SerializeField] ColorChangeOnCollision colorChangeOnCollision; //ColorChangeonCollision script object if any hit

    [SerializeField] float damage; //How much damage does this object do?
    [SerializeField] Health healthObjectHit; //Any health object hit if any

    public GameObject Boomerang { get => boomerang; set => boomerang = value; } //Property for boomerang
    public bool IsReturning { get => isReturning; set => isReturning = value; } //Property for isreturning

    void Update()
    {
        //If we press left mouse down
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //If isthrown or isreturning is true, go away, else check distance
            if (isThrown || IsReturning) return;
            {
                if (Boomerang == null) return;
                CheckDistance();
            }
        }

        //If isthrown is true
        if (isThrown)
        {
            // Set new position to move towards and apply to boomerang transform.
            Vector3 newPos = Vector3.MoveTowards(Boomerang.transform.position, throwPosition, throwSpeed * Time.deltaTime);
            Boomerang.transform.position = newPos;
            //Turn on the boomerangs mesh collider
            Boomerang.GetComponent<MeshCollider>().enabled = true;

            //If the boomerangs position is equal to the throw position
            if (Boomerang.transform.position == throwPosition)
            {
                //And if colorchange is not null
                if (colorChangeOnCollision != null)
                {
                    //Change color of the color change object and set it back to null
                    colorChangeOnCollision.ChangeColor();
                    colorChangeOnCollision = null;
                }
                //If there is a health object
                if (healthObjectHit != null)
                {
                    //Deal damage and set the health object back to null
                    healthObjectHit.TakeDamage(damage);
                    healthObjectHit = null;
                }

                //Isthrown to false, and isreturning to true
                isThrown = false;
                IsReturning = true;
            }
        }

        //Is isreturning
        if (IsReturning)
        {
            //Set the new position back to the boomerangs original position
            Vector3 newPos = Vector3.MoveTowards(Boomerang.transform.position, boomerangLocation.position, throwSpeed * Time.deltaTime);
            Boomerang.transform.position = newPos;

            //If boomerangs position is equal to original boomerang location
            if (Boomerang.transform.position == boomerangLocation.position)
            {
                //Set is returning to false, turn off rotator, set parent and rotation
                IsReturning = false;
                rotator.enabled = false;
                Boomerang.transform.parent = boomerangLocation;
                Boomerang.transform.rotation = boomerangRotation.rotation;
            }
        }
    }


    //Called from update, Checks distance then throws boomerang
    void CheckDistance()
    {
        //Raycast hit
        RaycastHit hit;

        //If raycast hit goes from boomerang location forward times boomerang distance and looking for the environment layer mask
        if (Physics.Raycast(boomerangLocation.transform.position, boomerangLocation.transform.forward, out hit, boomerangDistance, layerMask))
        {
            Boomerang.GetComponent<Rigidbody>().isKinematic = false;
            Boomerang.GetComponent<Rigidbody>().AddForce(boomerangLocation.forward * throwPower, ForceMode.Impulse);
            rotator.enabled = true;
            Boomerang.transform.parent = null;

            ////If colorchange is not null, set that reference
            //if (hit.transform.GetComponentInParent<ColorChangeOnCollision>() != null)
            //{
            //    colorChangeOnCollision = hit.transform.GetComponentInParent<ColorChangeOnCollision>();
            //}
            ////If health object is not null, set that reference
            //if (hit.transform.GetComponentInParent<Health>() != null)
            //{
            //    healthObjectHit = hit.transform.GetComponentInParent<Health>();
            //}

            ////Set throw postion to hit.point, set parent to null, turn rotator on an set isthrown to true so that it starts to travel.
            //throwPosition = hit.point;
            //Boomerang.transform.parent = null;
            //rotator.enabled = true;
            //isThrown = true;
        }
        //If the raycast does not hit anything, cast out by boomerang distance
        else
        {
            //Set throw position to boomerang.forward times boomerangdistance
            throwPosition = boomerangLocation.forward * boomerangDistance;
            //Set parent to null, enable rotator, and set isthrown to true so it starts to travel
            Boomerang.transform.parent = null;
            rotator.enabled = true;
            isThrown = true;
        }
    }

    //Picks up boomerang
    public void PickupBoomerang(GameObject boomerang)
    {
        //Sets boomerang to incoming boomerang
        Boomerang = boomerang;
        //Set parent, postion and rotation
        boomerang.transform.parent = boomerangLocation;
        Boomerang.transform.position = boomerangLocation.position;
        Boomerang.transform.rotation = boomerangRotation.rotation;
        //Turn on is kinematic so it doesnt fall, and turn off pick up trigger
        Boomerang.GetComponent<Rigidbody>().isKinematic = true;
        Boomerang.GetComponent<BoxCollider>().enabled = false;
    }
}