using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OvershieldPickup : MonoBehaviour
{
    [SerializeField] Overshield overshield;
    [SerializeField] Slider overShieldSlider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overshield.enabled = true;
            overShieldSlider.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
