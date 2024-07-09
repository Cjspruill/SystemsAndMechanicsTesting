using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Overshield : MonoBehaviour
{
    [SerializeField] bool useOvershieldText;    //Are we using an overshield text?
    [SerializeField] TextMeshProUGUI overshieldText;    //Overshield text if above is true
    [SerializeField] bool useOvershieldSlider;  //Are we using a slider?
    [SerializeField] Slider overshieldSlider;   //Slider if the above is true
    [SerializeField] float maxOvershield;   //Our max health value
    [SerializeField] float overshield;  //Our current health value
    [SerializeField] bool rechargeShield;   //Should we recharge our health?
    [SerializeField] float shieldRechargeRate;  //What is the recharge rate?

    [SerializeField] Health health; //A health reference, remove health when shield is depleted
    [SerializeField] bool isRecharging; //Are we currently recharging?
    [SerializeField] float rechargeTimer;
    [SerializeField] float rechargeTime;
    // Start is called before the first frame update
    void Start()
    {
        //If useOveshieldslider is true, set max value to maxovershield
        if (useOvershieldSlider)
        {
            overshieldSlider.maxValue = maxOvershield;
        }

        //Overshield equals maxhealth
       // overshield = maxOvershield;
    }

    // Update is called once per frame
    void Update()
    {
            rechargeTimer += Time.deltaTime;

        if (rechargeTimer >= rechargeTime)
            isRecharging = true;

        //If recharge overshield is true, give overshield with the overshield recharge rate
        if (rechargeShield && isRecharging)
        {
            GiveShieldHealth(shieldRechargeRate);
        }

        //If use health text is true, set health text.text to health value after rounding since its a float
        if (useOvershieldText)
        {
            overshieldText.text = Mathf.RoundToInt(overshield).ToString();
        }

        //If usehealthslider is true, set the health slider value to current health value
        if (useOvershieldSlider)
        {
            overshieldSlider.value = overshield;
        }
    }

    //Deals damage to this object
    public void TakeShieldDamage(float value)
    {
        //Health equals health minus value
        overshield -= value;

        //If health is less than or equal to 0, set it to 0
        if (overshield <= 0)
        {
            overshield = 0;
            health.TakeDamage(value);
        }

        isRecharging = false;
        rechargeTimer = 0;
    }

    //Gives health to this object
    public void GiveShieldHealth(float value)
    {
        //Health equals health plus value
        overshield += value;

        //If health is greater or equal to max health, set to maxhealth
        if (overshield >= maxOvershield)
            overshield = maxOvershield;
    }

    //Get health accessor
    public float GetHealth()
    {
        return overshield;
    }
}