using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFuse : MonoBehaviour
{
    [SerializeField] RotatingTitanCannon rotatingTitanCannon;

    private void OnMouseDown()
    {
        if(rotatingTitanCannon.IsLoaded)
        {
            rotatingTitanCannon.LightFuse();
        }
    }
}
