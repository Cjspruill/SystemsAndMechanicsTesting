using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] GameObject shield;

    [SerializeField] Transform shieldNotBlockingPosition;
    [SerializeField] Transform shieldBlockingPosition;
    [SerializeField] float blockingTransitionSpeed;
    [SerializeField] float blockingRotationSpeed;
    [SerializeField] bool isBlocking;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Transform>().CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            isBlocking = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            isBlocking = false;
        }
        }


        if (isBlocking)
        {
            RaiseShield();
        }
        else if(!isBlocking)
        {
            LowerShield();
        }
    }



    public void RaiseShield()
    {
        Vector3 newPos = Vector3.MoveTowards(shield.transform.position, shieldBlockingPosition.position, blockingTransitionSpeed * Time.deltaTime);
        shield.transform.position = newPos;
        Quaternion newRot = Quaternion.Slerp(shield.transform.rotation, shieldBlockingPosition.rotation, blockingRotationSpeed * Time.deltaTime);
        shield.transform.rotation = newRot;
    }

    public void LowerShield()
    {
        Vector3 newPos = Vector3.MoveTowards(shield.transform.position, shieldNotBlockingPosition.position, blockingTransitionSpeed * Time.deltaTime);
        shield.transform.position = newPos;
        Quaternion newRot = Quaternion.Slerp(shield.transform.rotation, shieldNotBlockingPosition.rotation, blockingRotationSpeed * Time.deltaTime);
        shield.transform.rotation = newRot;
    }
}
