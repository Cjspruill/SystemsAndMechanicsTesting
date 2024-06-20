using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] Transform cameraToFace; //What camera to face

    // Update is called once per frame
    void Update()
    {
        //Make this object face the camera using look at function
        transform.LookAt(cameraToFace);
    }
}
