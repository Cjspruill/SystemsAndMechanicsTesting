using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleGrenadeAttractor : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;

    [SerializeField]public BlackHoleGrenade blackHoleGrenade;
    [SerializeField] float speed;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (blackHoleGrenade != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, blackHoleGrenade.transform.position, speed * Time.deltaTime);
        }
    }
}
