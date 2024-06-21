using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    [SerializeField] public Trainer trainer; //Trainer reference
    [SerializeField] public GameObject pokemonCaptured; //Gameobject reference for the captured pokemon
    [SerializeField] int collisionCount;    //Collision counter

    // Start is called before the first frame update
    void Start()
    {
        //Destroy the pokeball after 15 seconds if it still exists in the world
        Destroy(gameObject, 15f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If collision count is greater than 0, go away
        if (collisionCount > 0) return;

        //If we collided with the ground
        if (collision.collider.tag == "Ground")
        {
            //Increase collision count
            collisionCount++;

            //If captured pokemon isnt null
            if (pokemonCaptured != null)
            {
                //Call trainer.releasePokemon, set the position to the pokeball and the rotation to the trainers rotation.
                trainer.ReleasePokemon(pokemonCaptured);
                pokemonCaptured.transform.position = transform.position;
                pokemonCaptured.transform.rotation = trainer.transform.rotation;
                //Turn on the captured pokemon and destroy the pokeball
                pokemonCaptured.SetActive(true);
                Destroy(gameObject);
            }
        }
        //If i collide with a pokemon
        if (collision.collider.tag == "Pokemon")
        {
            //Increase collision count
            collisionCount++;

            //If captured pokemon is null
            if (pokemonCaptured == null)
            {
                //Set captured pokemon to target that was hit, then call trainer.capture pokemon.
                pokemonCaptured = collision.collider.GetComponentInParent<Pokemon>().gameObject;
                trainer.CapturePokemon(pokemonCaptured);
                //Set captured pokemon to false and destroy the pokeball
                pokemonCaptured.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
