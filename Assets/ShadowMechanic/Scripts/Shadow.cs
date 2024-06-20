using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Shadow : MonoBehaviour
{
    [SerializeField] bool isHumanoid;
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float delayBetweenShadows;
    [SerializeField] float shadowStayTime;

    // Start is called before the first frame update
    void Start()
    {
        if(isHumanoid)
          InvokeRepeating("CreateHumanoidShadow", 0, delayBetweenShadows);
        else
        InvokeRepeating("CreateShadow", 0, delayBetweenShadows);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateShadow()
    {
        GameObject newShadow = Instantiate(objectToSpawn, transform.position, transform.rotation);

        Destroy(newShadow, shadowStayTime);
    }

    void CreateHumanoidShadow()
    {
        GameObject newShadow = Instantiate(transform.gameObject, transform.position, transform.rotation);
        newShadow.GetComponent<AICharacterControl>().enabled = false;
        newShadow.GetComponent<CapsuleCollider>().enabled = false;
        newShadow.GetComponent<Animator>().enabled = false;
        newShadow.GetComponent<ThirdPersonCharacter>().enabled = false;
        newShadow.GetComponent<NavMeshAgent>().enabled = false;
        newShadow.GetComponent<Shadow>().enabled = false;
        newShadow.GetComponent<Rigidbody>().useGravity = false;

        Destroy(newShadow, shadowStayTime);
    }
}
