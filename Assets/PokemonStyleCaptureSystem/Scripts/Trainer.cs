using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    [SerializeField] int pokeballs; //Number of current pokeballs
    [SerializeField] Transform pokeballSpawnLocation;   //Pokeball spawn location
    [SerializeField] GameObject pokeballPrefab; //Pokeball gameobject prefab
    [SerializeField] float throwForce;  //How hard are we throwing these pokeballs
    [SerializeField] bool hasCapturedPokemon;   //Do we have a captured pokemon?
    [SerializeField] List<GameObject> capturedPokemon; //Here is a list of our captured pokemons
    [SerializeField] int capturedPokemonIndex;  //Captured pokemon index for accesing above list
    [SerializeField] PokemonHud pokemonHud; //Pokemon hud for showing pokeball count and names

    // Update is called once per frame
    void Update()
    {
        //If we press the f key and have pokeballs greater than 0, throw a pokeball
        if(Input.GetKeyDown(KeyCode.F) && pokeballs > 0)
        {
            ThrowPokeball();
        }

        //If we scroll the mouse scroll wheel down and have a captured pokemon
        if(Input.GetAxis("Mouse ScrollWheel") > .1 && hasCapturedPokemon)
        {
            //If captured pokemon is greater than 0
            if (capturedPokemon.Count > 0) 
            { 
                //Decrease captured pokemon index, and update pokemon hud
                DecreaseCapturedPokemonIndex();
                pokemonHud.UpdatePokemonTextColor(pokemonHud.pokemonSlotTexts[capturedPokemonIndex]);
            }
        }
        //If we scroll the mouse scroll wheel up and have a captured pokemon
        else if(Input.GetAxis("Mouse ScrollWheel") < -.1 && hasCapturedPokemon)
        {
            //If captured pokemon is greater than 0
            if (capturedPokemon.Count > 0)
            {
                //Increase captured pokemon index, and update pokemon hud
                 IncreaseCapturedPokemonIndex();
                pokemonHud.UpdatePokemonTextColor(pokemonHud.pokemonSlotTexts[capturedPokemonIndex]);
            }
        }

        //If we press left mouse button and has a captured pokemon
        if(Input.GetKeyDown(KeyCode.Mouse0) && hasCapturedPokemon)
        {
            //Throw a pokeball with the captured pokemon at the specified index
            ThrowPokemonPokeball(capturedPokemon[capturedPokemonIndex]);
        }
    }

    //Throws a pokeball
    void ThrowPokeball()
    {
        //Decrease pokeballs
        pokeballs--;

        //Create new pokeball and throw it
        GameObject newPokeball = Instantiate(pokeballPrefab, pokeballSpawnLocation.position, pokeballSpawnLocation.rotation);
        newPokeball.GetComponent<Rigidbody>().AddForce(pokeballSpawnLocation.forward * throwForce, ForceMode.Impulse);

        //Set the trainer of pokeball to this
        newPokeball.GetComponent<Pokeball>().trainer = this;
    }

    //Throws a pokeball with a pokemon in it
    void ThrowPokemonPokeball(GameObject pokemon)
    {
        //Create a new pokeball and throws it
        GameObject newPokeball = Instantiate(pokeballPrefab, pokeballSpawnLocation.position, pokeballSpawnLocation.rotation);
        newPokeball.GetComponent<Rigidbody>().AddForce(pokeballSpawnLocation.forward * throwForce, ForceMode.Impulse);

        //Sets the pokeball trainer to this, and the captured pokemon to passed in pokemon
        newPokeball.GetComponent<Pokeball>().trainer = this;
        newPokeball.GetComponent<Pokeball>().pokemonCaptured = pokemon;
    }

    //Releases a pokemon from a pokeball
    public void ReleasePokemon(GameObject pokemon)
    {
        //Loop through captured pokemon and set names to empty
        for (int i = 0; i < capturedPokemon.Count; i++)
        {
        pokemonHud.UpdatePokemonTextName(i, "");
        }

        //Remove captured pokemon from list
        capturedPokemon.Remove(pokemon);

        //Loop through captured pokemon and update names
        for (int i = 0; i < capturedPokemon.Count; i++)
        {
            pokemonHud.UpdatePokemonTextName(i, capturedPokemon[i].GetComponent<Pokemon>().name);
        }
        
        //Decrease captured pokemon index
        DecreaseCapturedPokemonIndex();

        //Update next selected pokemon hud text and set color to red
        pokemonHud.UpdatePokemonTextColor(pokemonHud.pokemonSlotTexts[capturedPokemonIndex]);

        //If captured pokemon count equals 0, hascapturedpokemon is false
        if (capturedPokemon.Count == 0)
            hasCapturedPokemon = false;
    }

    //Captures a pokemon
    public void CapturePokemon(GameObject pokemon)
    {
        //Add pokemon to captured pokemon list
        capturedPokemon.Add(pokemon);

        //Loop through captured pokemon and update the text names
        for (int i = 0; i < capturedPokemon.Count; i++)
        {
            pokemonHud.UpdatePokemonTextName(i, capturedPokemon[i].GetComponent<Pokemon>().name);
        }

        //Update the text color of selected index and set has captured pokemon to true if it isnt yet set
        pokemonHud.UpdatePokemonTextColor(pokemonHud.pokemonSlotTexts[capturedPokemonIndex]);
        hasCapturedPokemon = true;
    }

    //Increases captured pokemon index
    void IncreaseCapturedPokemonIndex()
    {
        //Increase captured pokemon index
        capturedPokemonIndex++;
        //If captured pokemon index is greater or equal to captured pokemon set to 0
        if (capturedPokemonIndex >= capturedPokemon.Count)
            capturedPokemonIndex = 0;
    }

    //Decreases captured pokemon index
    void DecreaseCapturedPokemonIndex()
    {
        //Decreases captured pokemon index
        capturedPokemonIndex--;

        //If captured pokemon index is less than 0, set it to captured pokemon count
        if (capturedPokemonIndex < 0)
            capturedPokemonIndex = capturedPokemon.Count - 1;
        //If captured pokemon index is equal to 0, set it to 0
        if (capturedPokemon.Count == 0)      
            capturedPokemonIndex = 0;
        
    }

    //Get pokeball count
    public int GetPokeballs()
    {
        //Returns pokeball count
        return pokeballs;
    }
}
