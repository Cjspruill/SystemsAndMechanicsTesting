using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject selectedObject;

    [SerializeField] GameObject sword;


    public void Spawn(string name)
    {
        if(name == "Sword")
        {
            GameObject newObject = Instantiate(sword, Camera.main.ViewportToWorldPoint(Input.mousePosition) + transform.forward * 1, Quaternion.identity);
        }
    }
}
