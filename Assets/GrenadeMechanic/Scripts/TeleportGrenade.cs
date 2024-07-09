using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGrenade : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] bool alreadyLanded;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) return;
        if (alreadyLanded) return;
        alreadyLanded = true;


        audioSource.Play();
        transform.position = transform.position;

        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = transform.position + Vector3.up * 1;

        Invoke("EnableCharacterController", .1f);
    }

    void EnableCharacterController()
    {
        player.GetComponent<CharacterController>().enabled = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject,2);
    }
}
