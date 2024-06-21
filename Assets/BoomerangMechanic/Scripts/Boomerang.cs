using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] GameObject boomerang; //Game object reference to boomerang. Used to move boomerang
    [SerializeField] Transform boomerangLocation; //Original boomerang location
    [SerializeField] Transform boomerangRotation; //Original boomerang rotation
    [SerializeField] float boomerangDistance; //How far can we throw this thing
    [SerializeField] float throwSpeed; //Throw speed of this thing
    [SerializeField] LayerMask layerMask; //Layer mask for raycast check. Looking for environment layer

    [SerializeField] bool isThrown; //Bool that gets set when thrown
    [SerializeField] bool isReturning; //Bool that get set after hitting the middle point

    [SerializeField] Vector3 throwPosition; //This is where the boomerang is traveling to.
    [SerializeField] Rotator rotator; //Rotator on boomerang object. Gets turned on when thrown. And off when not.

    [SerializeField] ColorChangeOnCollision colorChangeOnCollision; //ColorChangeonCollision script object if any hit

    [SerializeField] float damage; //How much damage does this object do?
    [SerializeField] Health healthObjectHit; //Any health object hit if any

   
    // Update is called once per frame
    void Update()
    {
        //If we press left mouse down
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //If isthrown or isreturning is true, go away, else check distance
            if (isThrown || isReturning) return;
               CheckDistance();
        }

        //If isthrown is true
        if (isThrown)
        {
            //Set new position to move towards and apply to boomerang transform.
            Vector3 newPos = Vector3.MoveTowards(boomerang.transform.position, throwPosition, throwSpeed * Time.deltaTime);
            boomerang.transform.position = newPos;
            //Turn on the boomerangs mesh collider
            boomerang.GetComponent<MeshCollider>().enabled = true;

            //If the boomerangs position is equal to the throw position
            if(boomerang.transform.position == throwPosition)
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
                isReturning = true;
            }
        }

        //Is isreturning
        if (isReturning)
        {
            //Set the new position back to the boomerangs original position
            Vector3 newPos = Vector3.MoveTowards(boomerang.transform.position, boomerangLocation.position, throwSpeed * Time.deltaTime);
            boomerang.transform.position = newPos;

            //If boomerangs position is equal to original boomerang location
            if(boomerang.transform.position == boomerangLocation.position)
            {
                //Set is returning to false, turn off rotator, set parent and rotation
                isReturning = false;
                rotator.enabled = false;
                boomerang.transform.parent = boomerangLocation;
                boomerang.transform.rotation = boomerangRotation.rotation;
            }
        }
    }


    //Called from update, Checks distance then throws boomerang
    void CheckDistance()
    {
        //Raycast hit
        RaycastHit hit;

        //If raycast hit goes from boomerang location forward times boomerang distance and looking for the environment layer mask
        if(Physics.Raycast(boomerangLocation.transform.position,boomerangLocation.transform.forward,out hit, boomerangDistance,layerMask))
        {
            //If colorchange is not null, set that reference
            if (hit.transform.GetComponentInParent<ColorChangeOnCollision>() != null)
            {
                colorChangeOnCollision = hit.transform.GetComponentInParent<ColorChangeOnCollision>();
            }
            //If health object is not null, set that reference
            if (hit.transform.GetComponentInParent<Health>() != null)
            {
                healthObjectHit = hit.transform.GetComponentInParent<Health>();
            }
            
            //Set throw postion to hit.point, set parent to null, turn rotator on an set isthrown to true so that it starts to travel.
            throwPosition = hit.point;
            boomerang.transform.parent = null;
            rotator.enabled = true;
            isThrown = true;
        }
        //If the raycast does not hit anything, cast out by boomerang distance
        else
        {
            //Set throw position to boomerang.forward times boomerangdistance
            throwPosition = boomerangLocation.forward * boomerangDistance;
            //Set parent to null, enable rotator, and set isthrown to true so it starts to travel
            boomerang.transform.parent = null;
            rotator.enabled = true;
            isThrown = true;
        }
    }
}
