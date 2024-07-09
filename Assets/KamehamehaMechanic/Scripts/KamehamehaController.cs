using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamehamehaController : MonoBehaviour
{
    [SerializeField] GameObject kamehameha;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (kamehameha.activeInHierarchy) return;
            kamehameha.gameObject.SetActive(true);
          
        }
    }
}
