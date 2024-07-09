using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrenadeAttractor : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;

    [SerializeField] public MagnetGrenade magnetGrenade;
    [SerializeField] float speed;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (magnetGrenade != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, magnetGrenade.transform.position, speed * Time.deltaTime);
        }
    }
}
