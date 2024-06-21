using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ChangeDestinationOnArrival : MonoBehaviour
{
    [SerializeField] Transform origDestination; //The original destination
    [SerializeField] Transform newDestination;  //The new destination

    private void OnTriggerEnter(Collider other)
    {
        //If ai character is not null
        if (other.GetComponent<AICharacterControl>() != null)
        {
            //If ai character control target is equal to original destination set it to new destination 
            if (other.GetComponent<AICharacterControl>().target == origDestination)
                other.GetComponent<AICharacterControl>().target = newDestination;
            //Else set it to the orignal destination
            else
                other.GetComponent<AICharacterControl>().target = origDestination;
        }
    }
}
