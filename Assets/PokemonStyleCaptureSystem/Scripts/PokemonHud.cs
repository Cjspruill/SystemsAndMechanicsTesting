using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PokemonHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pokeballsText;
    [SerializeField]public TextMeshProUGUI[] pokemonSlotTexts;
    [SerializeField] Trainer trainer;

    // Start is called before the first frame update
    void Start()
    {
        ClearAllPokemonNameTexts();
    }

    // Update is called once per frame
    void Update()
    {
        pokeballsText.text = "Pokeballs: " + trainer.GetPokeballs().ToString();
    }

    void ClearAllPokemonNameTexts()
    {
        for (int i = 0; i < pokemonSlotTexts.Length; i++)
        {
            pokemonSlotTexts[i].text = "";
        }
    }

    public void UpdatePokemonTextColor(TextMeshProUGUI pokemonText)
    {
        for (int i = 0; i < pokemonSlotTexts.Length; i++)
        {
            pokemonSlotTexts[i].color = Color.black;
        }

        pokemonText.color = Color.red;
    }

    public void UpdatePokemonTextName(int index, string pokemonText)
    {
        pokemonSlotTexts[index].text = pokemonText;
    }
}
