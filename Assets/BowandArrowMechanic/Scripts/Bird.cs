using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float speed;   //How hard do we throw this object?

    private void Update()
    {
        //Move it by translating it forward by speed times time.delta time
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
