using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColorOnStart : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer; //Meshrenderer reference to change the color

    // Start is called before the first frame update
    void Start()
    {
        //Randomize color on start
        meshRenderer.material.color = Random.ColorHSV();
    }
}
