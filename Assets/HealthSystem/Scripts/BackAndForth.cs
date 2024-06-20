using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    [SerializeField] bool movingRight = true;   //Is this object moving right?
    [SerializeField] float speed;   //How fast this object moves

    // Update is called once per frame
    void Update()
    {
        //If moving right is true, move right by speed times time.deltatime
        if(movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        //Else if moving right is false, move left by speed times time.deltatime
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        //If other collider is equal to finish tag, flip moving right bool
        if(other.CompareTag("Finish"))
            movingRight = !movingRight;
    }
}
