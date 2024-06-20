using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnCollision : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer; //The meshrenderer to modify

    //Changes color
    public void ChangeColor()
    {
        //Sets meshrenderer.mater.color to a random color
        meshRenderer.material.color = Random.ColorHSV();
    }
}
