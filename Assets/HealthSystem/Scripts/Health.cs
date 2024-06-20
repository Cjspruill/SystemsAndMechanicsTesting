using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] bool useHealthText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] bool useHealthSlider;
    [SerializeField] Slider healthSlider;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] bool rechargeHealth;
    [SerializeField] float healthRechargeRate;



    // Start is called before the first frame update
    void Start()
    {
        if (useHealthSlider)
        {
            healthSlider.maxValue = maxHealth;
        }

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (rechargeHealth)
        {
            GiveHealth(healthRechargeRate);
        }

        if (useHealthText)
        {
            healthText.text = Mathf.RoundToInt(health).ToString();
        }

        if(useHealthSlider)
        {
            healthSlider.value = health;
        }
    }

    public void TakeDamage(float value)
    {
        health -= value;

        if(health <= 0)
        {
            health = 0;
        }
    }

    public void GiveHealth(float value)
    {
        health += value;

        if (health >= maxHealth)
            health = maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }
}
