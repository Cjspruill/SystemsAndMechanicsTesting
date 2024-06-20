using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    [SerializeField] public Trainer trainer;
    [SerializeField] public GameObject pokemonCaptured;
    [SerializeField] int collisionCount;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionCount > 0) return;

        if (collision.collider.tag == "Ground")
        {
            collisionCount++;

            if (pokemonCaptured != null)
            {
                trainer.ReleasePokemon(pokemonCaptured);
                pokemonCaptured.transform.position = transform.position;
                pokemonCaptured.transform.rotation = trainer.transform.rotation;
                pokemonCaptured.SetActive(true);
                Destroy(gameObject);
            }
        }
        if (collision.collider.tag == "Pokemon")
        {
            collisionCount++;

            if (pokemonCaptured == null)
            {
                pokemonCaptured = collision.collider.GetComponentInParent<Pokemon>().gameObject;
                trainer.CapturePokemon(pokemonCaptured);
                pokemonCaptured.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
