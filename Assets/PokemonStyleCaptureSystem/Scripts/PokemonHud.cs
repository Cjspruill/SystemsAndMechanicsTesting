using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PokemonHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pokeballsText; //Pokeballs text reference
    [SerializeField]public TextMeshProUGUI[] pokemonSlotTexts;  //Pokeballs slots texts refrence
    [SerializeField] Trainer trainer;   //Trainer reference

    // Start is called before the first frame update
    void Start()
    {
        //Clears all pokemon name texts
        ClearAllPokemonNameTexts();
    }

    // Update is called once per frame
    void Update()
    {
        //Set pokeballtext.text to pokeball count
        pokeballsText.text = "Pokeballs: " + trainer.GetPokeballs().ToString();
    }

    //Clears all pokemon name text elements
    void ClearAllPokemonNameTexts()
    {
        //For each slot in texts, set it to blank
        for (int i = 0; i < pokemonSlotTexts.Length; i++)
        {
            pokemonSlotTexts[i].text = "";
        }
    }

    //Updates pokemon text element color
    public void UpdatePokemonTextColor(TextMeshProUGUI pokemonText)
    {
        //For each slot, turn them all black first
        for (int i = 0; i < pokemonSlotTexts.Length; i++)
        {
            pokemonSlotTexts[i].color = Color.black;
        }

        //Then set selected pokemontext element color to red
        pokemonText.color = Color.red;
    }

    //Updates pokemon name text 
    public void UpdatePokemonTextName(int index, string pokemonText)
    {
        //At index that came in, update the slot text with the new pokemon text
        pokemonSlotTexts[index].text = pokemonText;
    }
}
