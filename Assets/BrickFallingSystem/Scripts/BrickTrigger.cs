using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTrigger : MonoBehaviour
{
    [SerializeField] BrickVersion2 brickVersion2;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brick") && other.gameObject == brickVersion2.SupportBrick)
        {
            brickVersion2.SupportBrick = null;
        }
    }
}
