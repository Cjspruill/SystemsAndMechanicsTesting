using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] bool useHealthText;    //Are we using a health text?
    [SerializeField] TextMeshProUGUI healthText;    //Health text if above is true
    [SerializeField] bool useHealthSlider;  //Are we using a slider?
    [SerializeField] Slider healthSlider;   //Slider if the above is true
    [SerializeField] float maxHealth;   //Our max health value
    [SerializeField] float health;  //Our current health value
    [SerializeField] bool rechargeHealth;   //Should we recharge our health?
    [SerializeField] float healthRechargeRate;  //What is the recharge rate?


    // Start is called before the first frame update
    void Start()
    {
        //If usehealthslider is true, set max value to maxhealth
        if (useHealthSlider)
        {
            healthSlider.maxValue = maxHealth;
        }

        //Health equals maxhealth
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //If recharge health is true, give health with the health recharge rate
        if (rechargeHealth)
        {
            GiveHealth(healthRechargeRate);
        }

        //If use health text is true, set health text.text to health value after rounding since its a float
        if (useHealthText)
        {
            healthText.text = Mathf.RoundToInt(health).ToString();
        }

        //If usehealthslider is true, set the health slider value to current health value
        if(useHealthSlider)
        {
            healthSlider.value = health;
        }
    }

    //Deals damage to this object
    public void TakeDamage(float value)
    {
        //Health equals health minus value
        health -= value;

        //If health is less than or equal to 0, set it to 0
        if(health <= 0)
        {
            health = 0;
        }
    }

    //Gives health to this object
    public void GiveHealth(float value)
    {
        //Health equals health plus value
        health += value;

        //If health is greater or equal to max health, set to maxhealth
        if (health >= maxHealth)
            health = maxHealth;
    }

    //Get health accessor
    public float GetHealth()
    {
        return health;
    }
}
