using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamehameha : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    [SerializeField] float sphereSizeLimit;
    [SerializeField] float sphereOriginalScale;

    [SerializeField] Transform cylinderPivot;
    [SerializeField] float speed;
    [SerializeField] float cylinderOrignalZScale;

    [SerializeField] bool growSphere = true;
    [SerializeField] bool growCylinder;

    [SerializeField] float stayTime;
    [SerializeField] float stayTimer;


    private void OnEnable()
    {
        growSphere = true;
        growCylinder = false;
    }

    private void Update()
    {

        if (growSphere)
        {
        Vector3 newSize = new Vector3(sphere.transform.localScale.x + 1, sphere.transform.localScale.y + 1, sphere.transform.localScale.z + 1);
        sphere.transform.localScale = Vector3.Lerp(sphere.transform.localScale, newSize, speed * Time.deltaTime);
        }

        if(sphere.transform.localScale.x >= sphereSizeLimit)
        {
                growSphere = false;
            growCylinder = true;
            if (growCylinder)
            {

                Vector3 newPos = new Vector3(cylinderPivot.localScale.x, cylinderPivot.localScale.y, cylinderPivot.localScale.z + 1);
                cylinderPivot.localScale = Vector3.Lerp(cylinderPivot.localScale, newPos, speed * Time.deltaTime);

                if (cylinderPivot.localScale.z >= 50)
                {
                    stayTimer += Time.deltaTime;

                    if (stayTimer >= stayTime)
                    {
                        growSphere = false;

                        sphere.transform.localScale = new Vector3(sphereOriginalScale, sphereOriginalScale, sphereOriginalScale);
                        cylinderPivot.transform.localScale = new Vector3(cylinderPivot.transform.localScale.x, cylinderPivot.transform.localScale.y, cylinderOrignalZScale);
                        sphere.SetActive(false);
                        stayTimer = 0;
                    }
                }
            }
        }

    }

}
