using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Shadow : MonoBehaviour
{
    [SerializeField] bool isHumanoid;   //Is this object a humanoid?
    [SerializeField] GameObject objectToSpawn;  //If is not a humanoid, what object are we spawning as the shadow?
    [SerializeField] float delayBetweenShadows; //How long between shadows?
    [SerializeField] float shadowStayTime;  //How long does each shadow stay?

    // Start is called before the first frame update
    void Start()
    {
        //If is humanoid, Create a humanoid shadow now, then every delay between shadows seconds
        if(isHumanoid)
          InvokeRepeating("CreateHumanoidShadow", 0, delayBetweenShadows);
        //Else if not humanoid, Create a shadow now, then every delay between shadows seconds
        else
        InvokeRepeating("CreateShadow", 0, delayBetweenShadows);
    }

    //Creates a shadow using a gameobject
    void CreateShadow()
    {
        //Create a newshadow
        GameObject newShadow = Instantiate(objectToSpawn, transform.position, transform.rotation);
        //Create it after shadow stay time
        Destroy(newShadow, shadowStayTime);
    }

    //Create humanoid shadow
    void CreateHumanoidShadow()
    {
        //Create new shadow
        GameObject newShadow = Instantiate(transform.gameObject, transform.position, transform.rotation);
        //Disable components not needed
        newShadow.GetComponent<AICharacterControl>().enabled = false;
        newShadow.GetComponent<CapsuleCollider>().enabled = false;
        newShadow.GetComponent<Animator>().enabled = false;
        newShadow.GetComponent<ThirdPersonCharacter>().enabled = false;
        newShadow.GetComponent<NavMeshAgent>().enabled = false;
        newShadow.GetComponent<Shadow>().enabled = false;
        newShadow.GetComponent<Rigidbody>().useGravity = false;
        //Destroy after shadow stay time
        Destroy(newShadow, shadowStayTime);
    }
}
