using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ChangeDestinationOnArrival : MonoBehaviour
{
    [SerializeField] Transform origDestination;
    [SerializeField] Transform newDestination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AICharacterControl>() != null)
        {
            if (other.GetComponent<AICharacterControl>().target == origDestination)
                other.GetComponent<AICharacterControl>().target = newDestination;
            else
                other.GetComponent<AICharacterControl>().target = origDestination;

        }
    }
}
