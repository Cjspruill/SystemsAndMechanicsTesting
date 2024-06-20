using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour
{

    [SerializeField] int pokeballs;
    [SerializeField] Transform pokeballSpawnLocation;
    [SerializeField] GameObject pokeballPrefab;
    [SerializeField] float throwForce;

    [SerializeField] bool hasCapturedPokemon;
    [SerializeField] List<GameObject> capturedPokemon;
    [SerializeField] int capturedPokemonIndex;

    [SerializeField] PokemonHud pokemonHud;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.F) && pokeballs > 0)
        {
            ThrowPokeball();
        }

        if(Input.GetAxis("Mouse ScrollWheel") > .1 && hasCapturedPokemon)
        {
            if (capturedPokemon.Count > 0) 
            { 
                DecreaseCapturedPokemonIndex();
                pokemonHud.UpdatePokemonTextColor(pokemonHud.pokemonSlotTexts[capturedPokemonIndex]);
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < -.1 && hasCapturedPokemon)
        {
            if (capturedPokemon.Count > 0)
            {
                 IncreaseCapturedPokemonIndex();
                pokemonHud.UpdatePokemonTextColor(pokemonHud.pokemonSlotTexts[capturedPokemonIndex]);
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && hasCapturedPokemon)
        {
            ThrowPokemonPokeball(capturedPokemon[capturedPokemonIndex]);
        }

    }

    void ThrowPokeball()
    {
        pokeballs--;
        GameObject newPokeball = Instantiate(pokeballPrefab, pokeballSpawnLocation.position, pokeballSpawnLocation.rotation);
        newPokeball.GetComponent<Rigidbody>().AddForce(pokeballSpawnLocation.forward * throwForce, ForceMode.Impulse);

        newPokeball.GetComponent<Pokeball>().trainer = this;
    }

    void ThrowPokemonPokeball(GameObject pokemon)
    {
        GameObject newPokeball = Instantiate(pokeballPrefab, pokeballSpawnLocation.position, pokeballSpawnLocation.rotation);
        newPokeball.GetComponent<Rigidbody>().AddForce(pokeballSpawnLocation.forward * throwForce, ForceMode.Impulse);

        newPokeball.GetComponent<Pokeball>().trainer = this;
        newPokeball.GetComponent<Pokeball>().pokemonCaptured = pokemon;
    }

    public void ReleasePokemon(GameObject pokemon)
    {

        for (int i = 0; i < capturedPokemon.Count; i++)
        {
        pokemonHud.UpdatePokemonTextName(i, "");
        }

        capturedPokemon.Remove(pokemon);

        for (int i = 0; i < capturedPokemon.Count; i++)
        {
            pokemonHud.UpdatePokemonTextName(i, capturedPokemon[i].GetComponent<Pokemon>().name);
        }
        
        DecreaseCapturedPokemonIndex();

        if (capturedPokemon.Count == 0)
            hasCapturedPokemon = false;
    }

    public void CapturePokemon(GameObject pokemon)
    {
        capturedPokemon.Add(pokemon);

        for (int i = 0; i < capturedPokemon.Count; i++)
        {
        pokemonHud.UpdatePokemonTextName(i, capturedPokemon[i].GetComponent<Pokemon>().name);
        }
        pokemonHud.UpdatePokemonTextColor(pokemonHud.pokemonSlotTexts[capturedPokemonIndex]);
        hasCapturedPokemon = true;
    }

    void IncreaseCapturedPokemonIndex()
    {
        capturedPokemonIndex++;
        if (capturedPokemonIndex >= capturedPokemon.Count)
            capturedPokemonIndex = 0;
    }

    void DecreaseCapturedPokemonIndex()
    {
        capturedPokemonIndex--;
        if (capturedPokemonIndex < 0)
            capturedPokemonIndex = capturedPokemon.Count - 1;
        if (capturedPokemon.Count == 0)      
            capturedPokemonIndex = 0;
        
    }

    public int GetPokeballs()
    {
        return pokeballs;
    }
}
