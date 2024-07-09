using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    [SerializeField] Bomb bomb; //Bomb reference

    private void OnTriggerEnter(Collider other)
    {
        //On trigger enter, if we hit a rigidbody, add it to the list of bomb.rigidbodies to be affected by force
        if(other.GetComponent<Rigidbody>()!=null)
        {
            bomb.RigidBodies.Add(other.GetComponent<Rigidbody>());
        }
    }
}
